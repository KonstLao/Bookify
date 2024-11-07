using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Bookings
{
    public sealed class TotalPrice
    {
        public decimal PriceForPeriod { get; init; }
        public decimal CleaningFee { get; init; }
        public decimal AmenitiesUpCharge { get; init; }

    }
}
