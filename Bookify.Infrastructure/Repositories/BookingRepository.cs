using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Repositories
{
    internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<List<Booking>> GetBookingsByApartmentIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<Booking>()
                .Where(entity => entity.ApartmentId == id)
                .ToListAsync(cancellationToken);
        }
    }
}
