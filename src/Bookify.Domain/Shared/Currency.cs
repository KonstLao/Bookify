using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Shared
{
	/// <summary>
	/// Структура описывает объект-значения "Валюта"
	/// </summary>
	public record Currency
	{
		/// <summary>
		/// Доллар США
		/// </summary>
		public static readonly Currency Usd = new Currency("USD");

		/// <summary>
		/// Евро
		/// </summary>
		public static readonly Currency Eur = new Currency("EUR");

		/// <summary>
		/// Российский рубль
		/// </summary>
		public static readonly Currency Rub = new Currency("RUB");
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="code"> Код валюты </param>
		private Currency(string code)
		{
			Code = code;
		}

		public static Currency FromCode(string code)
		{
			return All.FirstOrDefault(c => c.Code == code) ??
			       throw new ApplicationException("The currency code is invalid");
		}

		/// <summary>
		/// Код валюты
		/// </summary>
		public string Code { get; init; }

		/// <summary>
		/// Все валюты
		/// </summary>
		public static readonly IReadOnlyCollection<Currency> All = new[]
		{
			Usd,
			Eur,
			Rub
		};

		public static string RandomCurrency()
		{
			int randomInt = new Random().Next(0, 3);
			string code = All.ElementAt(randomInt).Code;
			return code;
		}
	}
}
