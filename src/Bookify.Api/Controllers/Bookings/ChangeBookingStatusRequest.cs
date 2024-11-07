using Bookify.Domain.Bookings;

namespace Bookify.Api.Controllers.Bookings
{
    public sealed record ChangeBookingStatusRequest(Guid Id);

}
