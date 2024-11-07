using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Bookings.ReserveBooking
{
    public class ReserveBookingCommandHandler(
        IUnitOfWork unitOfWork,
        IApartmentRepository apartmentRepository,
        IBookingRepository bookingRepository, 
        PricingService pricingService ) : ICommandHandler<ReserveBookingCommand, Guid>
    {
        public async Task<Result<Guid>>  Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
        {
            var apartment = await apartmentRepository.GetByIdAsync(request.ApartmentId);
            var booking = Booking.Reserve(apartment, request.UserId, request.DateFrom, request.DateTo, pricingService);
            var Apartmentbookings = await bookingRepository.GetBookingsByApartmentIdAsync(booking.ApartmentId, cancellationToken);
            foreach (var Apartmentbooking in Apartmentbookings)
            {
                if((Apartmentbooking.Status == BookingStatus.Confirmed || 
                    Apartmentbooking.Status == BookingStatus.Reserved) &&
                    (!Apartmentbooking.DateRange.IsDisjointWith(DateRange.GetRange(request.DateFrom, request.DateTo).Value)))
                {
                    return Result.Failure<Guid>(Error.DateJoinWith);
                }
            }
            bookingRepository.Add(booking);
            await unitOfWork.SaveChangesAsync();
            return Result.Success(booking.Id);
        }
    }
}
