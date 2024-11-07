using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments.ApartmentsEvents;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Apartments;


public sealed class Apartment : Entity
{
	private Apartment(Guid id ,
		Name name, 
		Description description,
		Address address,
		Money price,
		Money cleaningFee,
		List<Amenity> amenities, DateTime? lastBookedOnUtc) : base(id)
	{
		Name = name;
		Description = description;
		Address = address;
		Price = price;
		CleaningFee = cleaningFee;
		LastBookedOnUtc = lastBookedOnUtc;
		Amenities = amenities;
	}
	private Apartment() { }
    /// <summary>
    /// Наименование
    /// </summary>
    public Name Name { get; private set; }

	/// <summary>
	/// Описание
	/// </summary>
	public Description Description { get; private set; }

	/// <summary>
	/// Адрес
	/// </summary>
	public Address Address { get; private set; }

	/// <summary>
	/// Цена в день
	/// </summary>
	public Money Price { get; private set; }

	/// <summary>
	/// Стоимость уборки
	/// </summary>
	public Money CleaningFee { get; private set; }

	/// <summary>
	/// Дата последнего бронирования
	/// </summary>
	public DateTime? LastBookedOnUtc { get; internal set; }

    public ICollection<Amenity> Amenities { get; private set; } = new List<Amenity>();

    public static Result<Apartment> CreateApartment(Name name, Description description, Address address, Money price, Money cleaningFee, List<Amenity> amenities)
	{
		var apartment = new Apartment(Guid.NewGuid(), name, description, address, price, cleaningFee, amenities, null);
		apartment.RaiseDomainEvent(new ApartmentCreatedDomainEvent(apartment.Id));
		return apartment;
	}

}