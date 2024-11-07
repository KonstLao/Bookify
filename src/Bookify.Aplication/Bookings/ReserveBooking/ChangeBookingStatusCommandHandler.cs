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
    public class ChangeBookingStatusCommandHandler(IUnitOfWork unitOfWork,
        IBookingRepository bookingRepository) : ICommandHandler<ChangeBookingStatusCommand, BookingStatus>
    {
        public async Task<Result<BookingStatus>> Handle(ChangeBookingStatusCommand request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.BookingId);

            switch (request.NewStatus)
            {
                case BookingStatus.Confirmed:
                    booking.Confirm();
                    break;
                case BookingStatus.Rejected:
                    booking.Reject();
                    break;
                case BookingStatus.Cancelled:
                    booking.Cancel();
                    break;
                case BookingStatus.Completed:
                    booking.Complete();
                    break;
            }
            await unitOfWork.SaveChangesAsync();
            return Result.Success((booking.Status));
        }
    }
}
