using Bookify.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public void Add(Booking booking);
        public Task<List<Booking>> GetBookingsByApartmentIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
        public void Update(Booking booking);
    }
}
