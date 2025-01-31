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
        /// Высота сиденья.
        /// </summary>
        public double _seatHeight;

        /// <summary>
        /// Высота ножки.
        /// </summary>
        public double _legHeight;

        /// <summary>
        /// Длина ножки.
        /// </summary>
        public double _legLength;

        /// <summary>
        /// Ширина сиденья.
        /// </summary>
        public double _seatWidth;

        /// <summary>
        /// Строка наименования длины скамьи.
        /// </summary>
        public const string BenchLengthName = "Длина скамьи";

        /// <summary>
        /// Строка наименования высоты сиденья.
        /// </summary>
        public const string SeatHeightName = "Высота сиденья";

        /// <summary>
        /// Строка наименования высоты ножки.
        /// </summary>
        public const string LegHeightName = "Высота ножки";

        /// <summary>
        /// Строка наименования длины ножки.
        /// </summary>
        public const string LegLengthName = "Длина ножки";

        /// <summary>
        /// Строка наименования ширины сиденья.
        /// </summary>
        public const string SeatWidthName = "Ширина сиденья";

        //TODO: RSDN
        /// <summary>
        /// Минимальная и максимальная длина скамьи.
        /// </summary>
        private const double MinBenchLength = 100, MaxBenchLength = 200;

        /// <summary>
        /// Минимальная и максимальная высота сиденья.
        /// </summary>
        private const double MinSeatHeight = 10, MaxSeatHeight = 25;

        /// <summary>
        /// Минимальная и максимальная высота ножки.
        /// </summary>
        private const double MinLegHeight = 20, MaxLegHeight = 40;

        /// <summary>
        /// Минимальная и максимальная длина ножки.
        /// </summary>
        private const double MinLegLength = 20, MaxLegLength = 50;

        /// <summary>
        /// Минимальная и максимальная ширина сиденья.
        /// </summary>
        private const double MinSeatWidth = 30, MaxSeatWidth = 60;

        /// <summary>
        /// Свойство для длины скамьи.
        /// </summary>
        public double BenchLength
        {
            get => _benchLength;
            set
            {
                //TODO: RSDN
                _benchLength = Validator.SetValueInRange(value, MinBenchLength, MaxBenchLength, BenchLengthName);
            }
        }

        /// <summary>
        /// Свойство для высоты сиденья.
        /// </summary>
        public double SeatHeight
        {
            get => _seatHeight;
            set
            {
                //TODO: RSDN
                _seatHeight = Validator.SetValueInRange(value, MinSeatHeight, MaxSeatHeight, SeatHeightName);
            }
        }

        /// <summary>
        /// Свойство для высоты ножки.
        /// </summary>
        public double LegHeight
        {
            get => _legHeight;
            set
            {
                //TODO: RSDN
                _legHeight = Validator.SetValueInRange(value, MinLegHeight, MaxLegHeight, LegHeightName);
            }
        }

        /// <summary>
        /// Свойство для длины ножки.
        /// </summary>
        public double LegLength
        {
            get => _legLength;
            set
            {
                //TODO: RSDN
                _legLength = Validator.SetValueInRange(value, MinLegLength, MaxLegLength, LegLengthName);
            }
        }

        /// <summary>
        /// Свойство для ширины сиденья.
        /// </summary>
        public double SeatWidth
        {
            get => _seatWidth;
            set
            {
                //TODO: RSDN
                _seatWidth = Validator.SetValueInRange(value, MinSeatWidth, MaxSeatWidth, SeatWidthName);
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="benchLength">Длина скамьи.</param>
        /// <param name="seatHeight">Высота сиденья.</param>
        /// <param name="legHeight">Высота ножки.</param>
        /// <param name="legLength">Длина ножки.</param>
        /// <param name="seatWidth">Ширина сиденья.</param>
        public BenchParameters(
            double benchLength,
            double seatHeight,
            double legHeight,
            double legLength,
            double seatWidth
        )
        {
            BenchLength = benchLength;
            SeatHeight = seatHeight;
            LegHeight = legHeight;
            LegLength = legLength;
            SeatWidth = seatWidth;
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public BenchParameters()
        {
        }
    }
}
