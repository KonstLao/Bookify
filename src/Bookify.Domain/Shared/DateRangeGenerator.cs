using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Shared
{
    public class DateRangeGenerator
    {
        public DateOnly StartDate;
        public DateOnly EndDate;
        public int LenghthIndays;
        public void GenerateDateRange()
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-new Random().Next(1, 11)));
            EndDate = StartDate.AddDays(new Random().Next(1, 20));
            LenghthIndays = EndDate.DayNumber - StartDate.DayNumber;
        }
    }
}
