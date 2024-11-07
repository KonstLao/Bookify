using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings
{
	/// <summary>
	/// https://dotnet.microsoft.com/ru-ru/download
	/// Сервис расчета цены (пример проектирования доменного сервиса)
	/// </summary>
	public class PricingService
	{

		/// <summary>
		/// Рассчитать стоимость бронирования
		/// </summary>
		/// <returns></returns>
		public PricingDetails CalculatePrice(Apartment apartment, DateRange dateRange)
		{
			// Валюта апартаментов
			var currency = apartment.Price.Currency;


			// Стоимость за период
			var priceForPeriod = new Money(
				apartment.Price.Amount *
				(dateRange.LengthInDays), currency);

			// Доплата за удобства
			decimal percentageUpCharge = 0;
			// Перебор всех удобств
			foreach (var amenity in apartment.Amenities)
			{
				percentageUpCharge += amenity switch
				{
					Amenity.GardenView or Amenity.MountainView => 0.05m,
					Amenity.AirConditioning => 0.01m,
					Amenity.Parking => 0.01m,
					_ => 0
				};
			}

			// Доплата за удобства
			var amenitiesUpCharge = new Money(0, currency);
			// Если доплата за удобства больше нуля
			if (percentageUpCharge > 0)
			{
				// Рассчитываем доплату за удобства
				amenitiesUpCharge = new Money(
					priceForPeriod.Amount * percentageUpCharge,
					currency);

			}

			// Итоговая стоимость
			var totalPrice = new Money(0, currency);
			totalPrice += priceForPeriod;           // Стоимость за период
			totalPrice += apartment.CleaningFee;    // Стоимость уборки
			totalPrice += amenitiesUpCharge;        // Доплата за удобства


			// Вернуть детали ценообразования
			return new PricingDetails(priceForPeriod,
				apartment.CleaningFee, amenitiesUpCharge, totalPrice);
		}
		public static decimal GetTotalPrice(decimal priceForPeriod, decimal cleaningFee, decimal amenitiesUpCharge)
		{
			return (priceForPeriod + cleaningFee + amenitiesUpCharge);
		}
		public static decimal GetAmenitiesUpCharge(List<Amenity> amenities, decimal priceForPeriod) 
		{
            decimal percentageUpCharge = 0;
            // Перебор всех удобств
            foreach (var amenity in amenities)
            {
                percentageUpCharge += amenity switch
                {
                    Amenity.GardenView or Amenity.MountainView => 0.05m,
                    Amenity.AirConditioning => 0.01m,
                    Amenity.Parking => 0.01m,
                    _ => 0
                };
            }
			return percentageUpCharge * priceForPeriod;
        }

	}
}
