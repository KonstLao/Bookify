using Bookify.Aplication.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Bookings.GetBookings
{
    public record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>
    {

    }
}
