using Bookify.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Shared
{
    public class BookingStatusGenerator
    {
        public int status;
        public DateTime createdOnUtc;
        public DateTime? confirmedOnUtc;
        public DateTime? rejectedOnUtc;
        public DateTime? completedOnUc;
        public DateTime? cancelledOnUtc;

        public BookingStatusGenerator() { }

        public void Generate(DateOnly startDate, DateOnly endDate) 
        {
            var random = new Random();
            createdOnUtc = startDate.ToDateTime(TimeOnly.MinValue).AddDays(-random.Next(0, 3));
            status = 1;
            int randomInt = random.Next(1, 10);
            if (randomInt == 1 && startDate.ToDateTime(TimeOnly.MinValue).AddDays(1) < DateTime.Now)
            {
                cancelledOnUtc = createdOnUtc.AddDays(1);
                completedOnUc = cancelledOnUtc;
                status = 4;
                //rejectedOnUtc = null;
                //confirmedOnUtc = null;
            }
            if (randomInt == 2 && startDate.ToDateTime(TimeOnly.MinValue).AddDays(1) < DateTime.Now)
            {
                rejectedOnUtc = createdOnUtc.AddDays(1);
                completedOnUc = rejectedOnUtc;
                status = 3;
                //cancelledOnUtc = null;
                //confirmedOnUtc = null;
            }
            if (randomInt > 2)
            {
                var confirmedDate = createdOnUtc.AddDays(new Random().Next(0, 2));
                if (confirmedDate < DateTime.Now)
                {
                    confirmedOnUtc = confirmedDate;
                    status = 2;
                }
                if (endDate.ToDateTime(TimeOnly.MinValue) < DateTime.Now)
                {
                    completedOnUc = endDate.ToDateTime(TimeOnly.MinValue);
                    status = 5;
                }

            }

        }

    }
}
