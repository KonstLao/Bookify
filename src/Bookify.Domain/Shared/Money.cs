using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Shared
{
	/// <summary>
	/// Структура описывает объект-значения "Деньги"
	/// </summary>
	/// <param name="Amount"> Сумма </param>
	/// <param name="Currency"> Валюта </param>
	public record Money(decimal Amount, Currency Currency)
	{
		/// <summary>
		/// Перегрузка оператора сложения
		/// </summary>
		/// <param name="first"> Первое слагаемое </param>
		/// <param name="second"> Второе слагаемое </param>
		/// <returns> Сумма </returns>
		/// <exception cref="InvalidOperationException"> Валюты должны совпадать </exception>
		public static Money operator +(Money first, Money second)
		{
			if (first.Currency != second.Currency)
			{
				throw new InvalidOperationException("Currencies have to be equal");
			}
			return new Money(first.Amount + second.Amount, first.Currency);
		}
	}
}
