namespace Bench
{
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

		private void BuildButton_Click(object sender, EventArgs e)
		{
			var parameters = new Dictionary<TextBox, (string Name, Func<double, double> Validator)>
		{
			{ LengthTextBox, ("����� ������", value => Validator.SetValueInRange(value, 100, 200, "����� ������")) },
			{ HeightTextBox, ("������ ������", value => Validator.SetValueInRange(value, 50, 70, "������ ������")) },
			{ LegLengthTextBox, ("����� �����", value => Validator.SetValueInRange(value, 20, 30, "����� �����")) },
			{ LegWidthTextBox, ("������ �����", value => Validator.SetValueInRange(value, 20, 50, "������ �����")) },
			{ SeatHeightTextBox, ("������ �������", value => Validator.SetValueInRange(value, 10, 20, "������ �������")) }
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
				MessageBox.Show(string.Join(Environment.NewLine, errors), "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				var benchParameters = new BenchParameters(
					validatedValues[0], 
					validatedValues[1], 
					validatedValues[2], 
					validatedValues[3], 
					validatedValues[4]  
				);

				_benchWrapper.ConnectToKompas();
				_benchBuilder.BuildBench(benchParameters);

				MessageBox.Show("������ ������� ���������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
				ResetTextBoxColors();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// ���������� ����� ����������� � ����������� ���������.
		/// </summary>
		private void ResetTextBoxColors()
		{
			var textBoxes = new[] { LengthTextBox, HeightTextBox, LegLengthTextBox, LegWidthTextBox, SeatHeightTextBox };
			foreach (var textBox in textBoxes)
			{
				textBox.BackColor = SystemColors.Window;
			}
		}

	}
}

