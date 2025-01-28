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
		/// Обработчик нажатия на кнопку построения модели скамьи.
		/// </summary>
		private void BuildButton_Click(object sender, EventArgs e)
		{
			try
			{
				var parameters = new BenchParameters(
					Validator.ConvertToDouble(LengthTextBox.Text, "Длина скамьи"),
					Validator.ConvertToDouble(HeightTextBox.Text, "Высота скамьи"),
					Validator.ConvertToDouble(LegLengthTextBox.Text, "Длина ножки"),
					Validator.ConvertToDouble(LegWidthTextBox.Text, "Ширина ножки"),
					Validator.ConvertToDouble(SeatHeightTextBox.Text, "Ширина сиденья")
				);

				_benchWrapper.ConnectToKompas();
				_benchBuilder.BuildBench(parameters);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}

