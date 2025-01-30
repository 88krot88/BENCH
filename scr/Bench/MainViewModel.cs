namespace Bench
{
    public partial class MainViewModel : Form
    {
        /// <summary>
        /// Экземпляр класса BenchWrapper для подключения к Kompas-3D.
        /// </summary>
        private BenchWrapper _benchWrapper;

        /// <summary>
        /// Экземпляр класса BenchBuilder для построения скамьи.
        /// </summary>
        private BenchBuilder _benchBuilder;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MainViewModel()
        {
            InitializeComponent();
            _benchWrapper = new BenchWrapper();
            _benchBuilder = new BenchBuilder(_benchWrapper);
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<TextBox, (string Name, Func<double, double> Validator)>
            {
                { LengthTextBox, ("Длина скамьи", value => Validator.SetValueInRange(value, 100, 200, "Длина скамьи")) },
                { HeightTextBox, ("Высота скамьи", value => Validator.SetValueInRange(value, 50, 70, "Высота скамьи")) },
                { LegLengthTextBox, ("Длина ножки", value => Validator.SetValueInRange(value, 20, 30, "Длина ножки")) },
                { LegWidthTextBox, ("Ширина ножки", value => Validator.SetValueInRange(value, 30, 60, "Ширина ножки")) },
                { SeatHeightTextBox, ("Ширина сиденья", value => Validator.SetValueInRange(value, 30, 60, "Ширина сиденья")) }
            };

            var errors = new List<string>();
            var validatedValues = new List<double>();

            foreach (var (textBox, (paramName, validator)) in parameters)
            {
                try
                {
                    double value = Validator.ConvertToDouble(textBox.Text, paramName);
                    value = validator(value);

                    textBox.BackColor = Color.LightGreen;
                    validatedValues.Add(value);
                }
                catch (ArgumentException ex)
                {
                    textBox.BackColor = Color.LightCoral;
                    errors.Add(ex.Message);
                }
            }

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var benchParameters = new BenchParameters(
                    validatedValues[0], // Длина скамьи
                    validatedValues[1], // Высота скамьи
                    validatedValues[2], // Длина ножки
                    validatedValues[3], // Ширина ножки
                    validatedValues[4]  // Ширина сиденья
                );

                _benchWrapper.ConnectToKompas();
                _benchBuilder.BuildBench(benchParameters);

                MessageBox.Show("Скамья успешно построена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetTextBoxColors();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сбрасывает цвета текстбоксов в стандартное состояние.
        /// </summary>
        private void ResetTextBoxColors()
        {
            var textBoxes = new[] { LengthTextBox, HeightTextBox, LegLengthTextBox, LegWidthTextBox, SeatHeightTextBox };
            foreach (var textBox in textBoxes)
            {
                textBox.BackColor = SystemColors.Window;
            }
        }

        /// <summary>
        /// Минимальный размер окна.
        /// </summary>
        private void MainViewModel_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(450, 430);
        }
    }
}
