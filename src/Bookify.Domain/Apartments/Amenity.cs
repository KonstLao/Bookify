using Bookify.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartments
{
	//public record Amenity(
	//    string Name,
	//    string Description,
	//    Money Price);
	/// <summary>
	/// Дополнительные удобства,
	/// которые можно докупить при бронировании апартамента
	/// </summary>
	public enum Amenity
	{

		/// <summary>
		/// Wi-Fi
		/// </summary>
		WiFi = 1,

		/// <summary>
		/// Кондиционер
		/// </summary>
		AirConditioning = 2,

		/// <summary>
		/// Парковка
		/// </summary>
		Parking = 3,

		/// <summary>
		/// Возможность проживания с домашними животными
		/// </summary>
		PetFriendly = 4,

		/// <summary>
		/// Бассейн
		/// </summary>
		SwimmingPool = 5,

		/// <summary>
		/// Спортивный зал
		/// </summary>
		Gym = 6,

		/// <summary>
		/// СПА
		/// </summary>
		Spa = 7,

		/// <summary>
		/// Тераса
		/// </summary>
		Terrace = 8,

		/// <summary>
		/// Номер с видом на горы
		/// </summary>
		MountainView = 9,

		/// <summary>
		/// Номер с видом на красивый сад
		/// </summary>
		GardenView = 10


	}
	public class AmenitiesEx
	{
		public static List<Amenity> ToAmenityList(List<int> amenities)
		{
			// Используем Distinct() для удаления дубликатов
			var uniqueAmenities = amenities.Distinct().ToList();

			// Проверяем, что все значения в списке являются допустимыми
			if (uniqueAmenities.Any(id => !Enum.IsDefined(typeof(Amenity), id)))
			{
				throw new ArgumentException("Список содержит недопустимые значения.");
			}

			// Преобразуем список int в список Amenity
			return uniqueAmenities.Select(id => (Amenity)id).ToList();
		}


		public static List<Amenity> ConvertToAmenitiesList(string amenitiesString)
		{
			// 1. Удаляем ненужные кавычки вокруг названий enum
			amenitiesString = amenitiesString.Replace("\"\"", "\"")
											.Replace("\",\"", ",")
											.Trim('[', ']')// 2. Удаляем вложенные скобки
											.Trim('"');// 3. Удаляем лишние кавычки в начале и конце строк
           var amenityNames = amenitiesString.Split(',');// 4. Разбиваем строку на отдельные значения

			// проверка на остутствие удобств
            if (string.IsNullOrEmpty(amenitiesString) || amenitiesString == "0")
            {
                return new List<Amenity>(); // Возвращаем пустой список
            }

            // 5. Преобразуем список строк в список объектов Amenity
            List<Amenity> amenities = amenityNames.Select(name => (Amenity)Enum.Parse(typeof(Amenity), name.Trim(), true)).ToList();

			return amenities;
		}


	}
}


