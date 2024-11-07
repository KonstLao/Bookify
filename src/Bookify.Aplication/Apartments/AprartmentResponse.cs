using Bookify.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Apartments;
/// <summary>
/// Квартира/Аппартамент
/// </summary>
public sealed class ApartmentResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Название
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string? Currency { get; init; }

    /// <summary>
    /// Адрес
    /// </summary>
    public AddressResponse Address { get; set; }

    /// <summary>
    /// дата последнего бронирования
    /// </summary>
    public DateTime LastBookedonUTC { get; set; }

    public string Amenities { get; set; }
    public List<Amenity> AmenitiesList { get; set; }
}

