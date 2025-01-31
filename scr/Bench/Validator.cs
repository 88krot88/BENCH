using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bench
{
	/// <summary>
	/// Валидирует значения параметров скамьи. 
	/// </summary>
	public static class Validator
	{
		/// <summary>
		/// Проверяет значение на вхождение в указанный диапазон.
		/// </summary>
		/// <param name="value">Значение на проверку.</param>
		/// <param name="minimumValue">Минимальная граница допустимого диапазона значений.</param>
		/// <param name="maximumValue">Максимальная граница допустимого диапазона значений.</param>
		/// <param name="parameterName">Имя параметра.</param>
		/// <returns>Валидированное значение.</returns>
		public static double SetValueInRange(
			double value, 
			double minimumValue, 
			double maximumValue, 
			string parameterName
		)
		{
			if (value >= minimumValue && value <= maximumValue)
			{
				return value;
			}
            //TODO: RSDN
            throw new ArgumentException($"Параметр '{parameterName}' должен быть в пределах {minimumValue} - {maximumValue}.");
		}

		/// <summary>
		/// Преобразовывает строку в вещественное число.
		/// чтобы оно входило в заданный диапазон значений. 
		/// </summary>
		/// <param name="userInput">Значение, введенное пользователем в текстовое поле.</param>
		/// <param name="parameterName">Имя параметра.</param>
		/// <returns>Вещественное число.</returns>
		/// <exception cref="ArgumentException"></exception>
		public static double ConvertToDouble(string userInput,string parameterName)
		{
			if (double.TryParse(userInput, out double parsedValue))
			{
				return parsedValue;
			}

			throw new ArgumentException($"Параметр '{parameterName}' должен быть числом.");
		}
	}
}
