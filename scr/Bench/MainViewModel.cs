namespace Bench
{
	/// <summary>
	/// �������� ������ �������������.
	/// </summary>
	public partial class MainViewModel : Form
    {
        /// <summary>
        /// ��������� ������ BenchWrapper ��� ����������� � Kompas-3D.
        /// </summary>
        private BenchWrapper _benchWrapper;

        /// <summary>
        /// ��������� ������ BenchWrapper ��� ���������� ������.
        /// </summary>
        private BenchBuilder _benchBuilder;

        /// <summary>
        /// �����������.
        /// </summary>
        public MainViewModel()
        {
            InitializeComponent();
            _benchWrapper = new BenchWrapper();
            _benchBuilder = new BenchBuilder(_benchWrapper);
        }

        /// <summary>
        /// ���������� ������� ����� �� ������ ��� ��������� � ��������� �������� ���������� ������.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� �������.</param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            var errors = new List<string>();
            var benchParameters = new BenchParameters();

            // ������ ���������� ������ � ��������������� ��������� �����
            var properties = new (
                Action<BenchParameters, 
                double> SetProperty, 
                string UserInput, 
                TextBox TextBox, 
                string ParameterName
            )[]
            {
                ((instance, value) => 
                    instance.BenchLength = value, 
                    LengthTextBox.Text, LengthTextBox, 
                    BenchParameters.BenchLengthName),
                ((instance, value) => 
                    instance.SeatHeight = value, 
                    SeatHeightTextBox.Text, 
                    SeatHeightTextBox,
                    BenchParameters.SeatHeightName),
                ((instance, value) => 
                    instance.LegHeight = value, 
                    LegHeightTextBox.Text, 
                    LegHeightTextBox, 
                    BenchParameters.LegHeightName),
                ((instance, value) => 
                    instance.LegLength = value, 
                    LegLengthTextBox.Text, 
                    LegLengthTextBox, 
                    BenchParameters.LegLengthName),
                ((instance, value) => 
                    instance.SeatWidth = value, 
                    SeatWidthTextBox.Text, 
                    SeatWidthTextBox, 
                    BenchParameters.SeatWidthName)
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
                MessageBox.Show(
                    string.Join(Environment.NewLine, errors), 
                    "������ �����", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
            else
            {
                _benchWrapper.ConnectToKompas();
                _benchBuilder.BuildBench(benchParameters);
            }
        }

        /// <summary>
        /// ����� ��� ��������� � ��������� �������� ��� ��������� ������.
        /// </summary>
        /// <param name="property">������ � ��������� ��������� ��������, ������� �����, ��������� ����� � ������ ���������.</param>
        /// <param name="benchParameters">������ ������, � ������� ����� ����������� ��������.</param>
        /// <param name="errors">������ ������, � ������� ����������� ��������� �� �������.</param>
        /// <returns>���������� true, ���� �������� ����������, ����� false.</returns>
        private bool ValidateAndSetProperty(
            (Action<BenchParameters, 
             double> SetProperty, 
             string UserInput, 
             TextBox TextBox, 
             string ParameterName) property, 
            BenchParameters benchParameters, 
            List<string> errors
        )
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
