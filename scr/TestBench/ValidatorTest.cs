using NUnit.Framework;
using System;
using System.Globalization;

namespace Bench.UnitTests
{
	[TestFixture]
	public class ValidatorTests
	{
		[TestCase(50, 0, 100, "TestParam", TestName = "Число в пределах диапазона")]
		[TestCase(0, 0, 100, "TestParam", TestName = "Число на нижней границе диапазона")]
		[TestCase(100, 0, 100, "TestParam", TestName = "Число на верхней границе диапазона")]
		public void SetValueInRange_ValidValue_ReturnsSameValue(double value, double min, double max, string paramName)
		{
			var result = Validator.SetValueInRange(value, min, max, paramName);
			Assert.That(result, Is.EqualTo(value));
		}

		[TestCase(-1, 0, 100, "TestParam", TestName = "Число меньше минимального предела")]
		[TestCase(101, 0, 100, "TestParam", TestName = "Число больше максимального предела")]
		public void SetValueInRange_InvalidValue_ThrowsArgumentException(double value, double min, double max, string paramName)
		{
			var ex = Assert.Throws<ArgumentException>(() => Validator.SetValueInRange(value, min, max, paramName));
			Assert.That(ex.Message, Does.Contain(paramName));
		}

		[TestCase("123.45", 123.45, "TestParam", TestName = "Корректное плавающее число")]
		[TestCase("-75.89", -75.89, "TestParam", TestName = "Корректное отрицательное число")]
		[TestCase("0", 0, "TestParam", TestName = "Ноль")]
		public void ConvertToDouble_ValidInputs_ReturnsDouble(string input, double expected, string paramName)
		{
			var result = double.Parse(input, CultureInfo.InvariantCulture);
			Assert.That(result, Is.EqualTo(expected).Within(0.0001), "Парсинг числа работает некорректно");
		}

		[TestCase("abc", "TestParam", TestName = "Некорректная строка - не число")]
		[TestCase("", "TestParam", TestName = "Пустая строка")]
		[TestCase("  ", "TestParam", TestName = "Строка из пробелов")]
		public void ConvertToDouble_InvalidString_ThrowsArgumentException(string input, string paramName)
		{
			var ex = Assert.Throws<ArgumentException>(() => Validator.ConvertToDouble(input, paramName));
			Assert.That(ex.Message, Does.Contain(paramName));
		}
	}
}