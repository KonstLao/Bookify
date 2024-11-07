using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Bookings.GetBookings
{
    public record GetAllBookingsQuery(Guid UserId) : IQuery<List<BookingResponse>>
    {
    }
}
