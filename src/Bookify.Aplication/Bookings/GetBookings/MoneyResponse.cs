using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Bookings.GetBookings
{
	/// <summary>
	/// Деньги
	/// </summary>
	public class MoneyResponse
	{
		/// <summary>
		/// Количество денег (Цена)
		/// </summary>
		public decimal Amount { get; set; }
		/// <summary>
		/// Валюта
		/// </summary>
		public string Currency { get; set; }
	}
}
