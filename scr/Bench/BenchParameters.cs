namespace Bench
{
	/// <summary>
	/// Параметры скамьи.
	/// </summary>
	public class BenchParameters
	{
		/// <summary>
		/// Длина скамьи.
		/// </summary>
		public double _benchLength;

		/// <summary>
		/// Ширина скамьи.
		/// </summary>
		public double _benchHeight;

		/// <summary>
		/// Длина ножки скамьи.
		/// </summary>
		public double _legLength;

		/// <summary>
		/// Ширина ножки скамьи.
		/// </summary>
		public double _legWidth;

		/// <summary>
		/// Длина сиденья скамьи.
		/// </summary>
		public double _seatLength;

		/// <summary>
		/// Ширина сиденья скамьи.
		/// </summary>
		public double _seatWidth;

		/// <summary>
		/// Минимальная длина скамьи.
		/// </summary>
		private const double MinBenchLength = 100;

		/// <summary>
		/// Максимальная длина скамьи.
		/// </summary>
		private const double MaxBenchLength = 200;

		/// <summary>
		/// Минимальная ширина скамьи.
		/// </summary>
		private const double MinBenchHeight = 50;

		/// <summary>
		/// Максимальная ширина скамьи.
		/// </summary>
		private const double MaxBenchHeight = 70;

		/// <summary>
		/// Минимальная длина ножки скамьи.
		/// </summary>
		private const double MinLegLength = 20;

		/// <summary>
		/// Максимальная длина ножки скамьи.
		/// </summary>
		private const double MaxLegLength = 30;

		/// <summary>
		/// Минимальная ширина ножки скамьи.
		/// </summary>
		private const double MinLegWidth = 30;

		/// <summary>
		/// Максимальная ширина ножки скамьи.
		/// </summary>
		private const double MaxLegWidth = 60;

		/// <summary>
		/// Минимальная длина ширина сиденья.
		/// </summary>
		private const double MinSeatWidth = 30;

		/// <summary>
		/// Максимальная длина ширина сиденья.
		/// </summary>
		private const double MaxSeatWidth = 60;

		/// <summary>
		/// Свойство для длины скамьи.
		/// </summary>
		public double BenchLength 
		{
			get => _benchLength;
			set
			{
				_benchLength = Validator.SetValueInRange(
					value,
					MinBenchLength,
					MaxBenchLength,
					"Длина скамейки"
				);
			}
		}

		/// <summary>
		/// Свойство для высоты скамьи.
		/// </summary>
		public double BenchHeight 
		{
			get => _benchHeight;
			set
			{
				_benchHeight = Validator.SetValueInRange(
					value,
					MinBenchHeight,
					MaxBenchHeight,
					"Высота скамейки"
				);
			}
		}

		/// <summary>
		/// Свойство для длины ножки скамьи.
		/// </summary>
		public double LegLength 
		{ 
			get => _legLength;
			set 
			{ 
				_legLength = Validator.SetValueInRange(
					value,
					MinLegLength,
					MaxLegLength,
					"Длина ножек"
				);
			}
		}

		/// <summary>
		/// Свойство для ширины ножки скамьи.
		/// </summary>
		public double LegWidth 
		{ 
			get => _legWidth;
			set 
			{
				_legWidth = Validator.SetValueInRange(
					value,
					MinLegWidth,
					MaxLegWidth,
					"Ширина ножек"
				);
			}
		}

		/// <summary>
		/// Свойство для ширины сиденья скамьи.
		/// </summary>
		public double SeatWidth 
		{
			get => _seatWidth;
			set
			{
				_seatWidth = Validator.SetValueInRange(
					value,
					MinSeatWidth,
					MaxSeatWidth,
					"Ширина сиденья"
				);
			}
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="benchLength">Длина скамьи.</param>
		/// <param name="benchHeight">Высота скамьи.</param>
		/// <param name="legLength">Длина ножки.</param>
		/// <param name="legWidth">Ширина ножки.</param>
		/// <param name="seatWidth">Ширина сиденья.</param>
		public BenchParameters(
			double benchLength,
			double benchHeight,
			double legLength,
			double legWidth,
			double seatWidth
		)
		{
			BenchLength = benchLength;
			BenchHeight = benchHeight;
			LegLength = legLength;
			LegWidth = legWidth;
			SeatWidth = seatWidth;
		}
	}
}
