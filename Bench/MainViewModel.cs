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

		/// <summary>
		/// ���������� ������� �� ������ ���������� ������ ������.
		/// </summary>
		private void BuildButton_Click(object sender, EventArgs e)
		{
			try
			{
				var parameters = new BenchParameters(
					Validator.ConvertToDouble(LengthTextBox.Text, "����� ������"),
					Validator.ConvertToDouble(HeightTextBox.Text, "������ ������"),
					Validator.ConvertToDouble(LegLengthTextBox.Text, "����� �����"),
					Validator.ConvertToDouble(LegWidthTextBox.Text, "������ �����"),
					Validator.ConvertToDouble(SeatHeightTextBox.Text, "������ �������")
				);

				_benchWrapper.ConnectToKompas();
				_benchBuilder.BuildBench(parameters);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}

