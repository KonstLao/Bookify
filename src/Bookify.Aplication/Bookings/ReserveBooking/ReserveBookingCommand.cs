using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Bookings.ReserveBooking
{
     public record ReserveBookingCommand(Guid UserId, Guid ApartmentId, DateOnly DateFrom, DateOnly DateTo) : ICommand<Guid>
    {

    }
}
