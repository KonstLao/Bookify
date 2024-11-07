using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookify.Aplication.Bookings.ReserveBooking
{
    public record ChangeBookingStatusCommand(Guid BookingId, BookingStatus NewStatus) : ICommand<BookingStatus>
    {
    }
}
