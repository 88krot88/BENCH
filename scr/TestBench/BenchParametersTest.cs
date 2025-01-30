using NUnit.Framework;
using System;

namespace Bench.UnitTests
{
	[TestFixture]
	public class BenchParametersTests
	{
		private BenchParameters _benchParameters;

		[SetUp]
		public void InitializeBenchParameters()
		{
			_benchParameters = new BenchParameters();
		}

		[Test(Description = "Позитивный тест конструктора без параметров")]
		public void TestBenchParameters_DefaultConstructor_DoesNotThrow()
		{

			for (var i = 0; i < 100; ++i)
			{
				Assert.DoesNotThrow(() => new BenchParameters());
			}
		}

		[Test(Description = "Позитивный тест установки и получения длины скамьи")]
		public void TestBenchLength_Get_CorrectValue()
		{
			_benchParameters.BenchLength = 150;

			var expected = 150;
			var actual = _benchParameters.BenchLength;

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test(Description = "Негативный тест установки некорректной длины скамьи")]
		public void TestBenchLength_SetIncorrectValue_ThrowException()
		{
			Assert.Throws<ArgumentException>(() => _benchParameters.BenchLength = 50);
		}

		[Test(Description = "Позитивный тест установки и получения высоты сиденья")]
		public void TestSeatHeight_Get_CorrectValue()
		{
			_benchParameters.SeatHeight = 20;

			var expected = 20;
			var actual = _benchParameters.SeatHeight;

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test(Description = "Негативный тест установки некорректной высоты сиденья")]
		public void TestSeatHeight_SetIncorrectValue_ThrowException()
		{
			Assert.Throws<ArgumentException>(() => _benchParameters.SeatHeight = 5);
		}

		[Test(Description = "Позитивный тест установки и получения высоты ножки")]
		public void TestLegHeight_Get_CorrectValue()
		{
			_benchParameters.LegHeight = 30;
			var expected = 30;

			var actual = _benchParameters.LegHeight;

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test(Description = "Негативный тест установки некорректной высоты ножки")]
		public void TestLegHeight_SetIncorrectValue_ThrowException()
		{
			Assert.Throws<ArgumentException>(() => _benchParameters.LegHeight = 10);
		}

		[Test(Description = "Позитивный тест установки и получения длины ножки")]
		public void TestLegLength_Get_CorrectValue()
		{
			_benchParameters.LegLength = 40;
			var expected = 40;

			var actual = _benchParameters.LegLength;

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test(Description = "Негативный тест установки некорректной длины ножки")]
		public void TestLegLength_SetIncorrectValue_ThrowException()
		{
			Assert.Throws<ArgumentException>(() => _benchParameters.LegLength = 10);
		}

		[Test(Description = "Позитивный тест установки и получения ширины сиденья")]
		public void TestSeatWidth_Get_CorrectValue()
		{
			_benchParameters.SeatWidth = 50;

			var expected = 50;
			var actual = _benchParameters.SeatWidth;

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test(Description = "Негативный тест установки некорректной ширины сиденья")]
		public void TestSeatWidth_SetIncorrectValue_ThrowException()
		{
			Assert.Throws<ArgumentException>(() => _benchParameters.SeatWidth = 20);
		}
	}
}
