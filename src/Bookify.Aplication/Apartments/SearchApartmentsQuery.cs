using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;

namespace Bookify.Aplication.Apartments
{
    public record SearchApartmentsQuery(DateOnly StartDate, DateOnly EndDate) : IQuery<List<ApartmentResponse>>
    {

    }
}
