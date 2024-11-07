using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings;


public sealed class Booking : Entity
{

	private Booking() { }

	private Booking(
		Guid id,
		Guid apartmentId,
		Guid userId,
		DateRange dateRange,
		Money priceForPeriod,
		Money cleaningFee,
		Money amenitiesUpCharge,
		Money totalPrice,
		BookingStatus status,
		DateTime createdOnUtc)
		: base(id)
	{
		ApartmentId = apartmentId;
		UserId = userId;
		DateRange = dateRange;
		PriceForPeriod = priceForPeriod;
		CleaningFee = cleaningFee;
		AmenitiesUpCharge = amenitiesUpCharge;
		TotalPrice = totalPrice;
		Status = status;
		CreatedOnUtc = createdOnUtc;
	}


	public Guid ApartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public DateRange DateRange { get; private set; } 
	//public DateOnly EndDate { get; private set; } 
	public Money PriceForPeriod { get; private set; }
    public Money CleaningFee { get; private set; }
    public Money AmenitiesUpCharge { get; private set; }
    public Money TotalPrice { get; private set; }
	public BookingStatus Status { get; private set; }
	public DateTime CreatedOnUtc { get; private set; }
	public DateTime? ConfirmedOnUtc { get; private set; }

    public DateTime? RejectedOnUtc { get; private set; } 
    public DateTime? CompletedOnUtc { get; private set; } 
    public DateTime? CancelledOnUtc { get; private set; } 

	/// <summary>
	/// Статический фабричный метод для создания нового бронирования
	/// </summary>
	/// <param name="apartment"></param>
	/// <param name="userId"></param>
	/// <param name="startDate"></param>
	/// <param name="endDate"></param>
	/// <param name="pricingService"></param>
	/// <returns></returns>
	public static Booking Reserve(
	    Apartment apartment,
	    Guid userId,
	    DateOnly startDate,
        DateOnly endDate,
	    PricingService pricingService)
    {
		DateRange dateRange = DateRange.GetRange(startDate, endDate).Value;

        var pricingDetails = pricingService.CalculatePrice(apartment, dateRange);
	    var booking = new Booking(
		    Guid.NewGuid(),
		    apartment.Id,
		    userId,
		    dateRange,
		    pricingDetails.PriceForPeriod,
		    pricingDetails.CleaningFee,
		    pricingDetails.AmenitiesUpCharge,
		    pricingDetails.TotalPrice,
		    BookingStatus.Reserved,
		    DateTime.Now);

		apartment.LastBookedOnUtc = DateTime.UtcNow;


		booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));

		return booking;
	}

	/// <summary>
	/// Подтвердить бронирование
	/// </summary>
	/// <returns></returns>
	public Result Confirm()
	{
		if (Status != BookingStatus.Reserved)
		{
			return Result.Failure(BookingErrors.NotReserved);
		}

		Status = BookingStatus.Confirmed;

		RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));

		return Result.Success();
	}

    /// <summary>
    /// отмена бронирования со стороны сервиса
    /// </summary>
    /// <returns></returns>
    public Result Reject()
    {
        if (Status != BookingStatus.Reserved && Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.RejectErr);
        }

        Status = BookingStatus.Rejected;

		RaiseDomainEvent(new BookingRejectedDomainEvent(Id));

		return Result.Success();
    }

    /// <summary>
    /// Отмена бронирования со стороны пользователя
    /// </summary>
    /// <returns></returns>
    public Result Cancel()
    {
        if (Status != BookingStatus.Reserved || Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.CancellErr);
        }

        Status = BookingStatus.Cancelled;

		RaiseDomainEvent(new BookingCancelledDomainEvent(Id));

		return Result.Success();
    }

    /// <summary>
    /// завершение бронирования
    /// </summary>
    /// <returns></returns>
    public Result Complete()
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        Status = BookingStatus.Completed;

        RaiseDomainEvent(new BookingComplitedDomainEvent(Id));

        return Result.Success();
    }

}
