namespace Bench
{
    public partial class MainViewModel : Form
    {
        /// <summary>
        /// Экземпляр класса BenchWrapper для подключения к Kompas-3D.
        /// </summary>
        private BenchWrapper _benchWrapper;

        /// <summary>
        /// Экземпляр класса BenchWrapper для построения скамьи.
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

        /// <summary>
        /// Обработчик события клика по кнопке для валидации и установки значений параметров скамьи.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            var errors = new List<string>();
            var benchParameters = new BenchParameters();

            // Список параметров скамьи и соответствующих текстовых полей
            var properties = new (Action<BenchParameters, double> SetProperty, string UserInput, TextBox TextBox, string ParameterName)[]
            {
                ((obj, value) => obj.BenchLength = value, LengthTextBox.Text, LengthTextBox, BenchParameters.BenchLengthName),
                ((obj, value) => obj.SeatHeight = value, SeatHeightTextBox.Text, SeatHeightTextBox, BenchParameters.SeatHeightName),
                ((obj, value) => obj.LegHeight = value, LegHeightTextBox.Text, LegHeightTextBox, BenchParameters.LegHeightName),
                ((obj, value) => obj.LegLength = value, LegLengthTextBox.Text, LegLengthTextBox, BenchParameters.LegLengthName),
                ((obj, value) => obj.SeatWidth = value, SeatWidthTextBox.Text, SeatWidthTextBox, BenchParameters.SeatWidthName)
            };

            foreach (var property in properties)
            {
                property.TextBox.BackColor = Color.LightGreen;

                if (!ValidateAndSetProperty(property, benchParameters, errors))
                {
                    property.TextBox.BackColor = Color.LightCoral;
                }
            }

            if (errors.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _benchWrapper.ConnectToKompas();
                _benchBuilder.BuildBench(benchParameters);
            }
        }

        /// <summary>
        /// Метод для валидации и установки значения для параметра скамьи.
        /// </summary>
        /// <param name="property">Кортеж с действием установки значения, строкой ввода, текстовым полем и именем параметра.</param>
        /// <param name="benchParameters">Объект скамьи, в который будут установлены значения.</param>
        /// <param name="errors">Список ошибок, в который добавляются сообщения об ошибках.</param>
        /// <returns>Возвращает true, если значение корректное, иначе false.</returns>
        private bool ValidateAndSetProperty((Action<BenchParameters, double> SetProperty, string UserInput, TextBox TextBox, string ParameterName) property, BenchParameters benchParameters, List<string> errors)
        {
            try
            {
                var value = Validator.ConvertToDouble(property.UserInput, property.ParameterName);
                property.SetProperty(benchParameters, value);
                return true;
            }
            catch (ArgumentException ex)
            {
                errors.Add(ex.Message);
                return false;
            }
        }
    }
}
