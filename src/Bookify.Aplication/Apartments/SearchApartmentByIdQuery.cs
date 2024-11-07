using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Apartments
{
    public record SearchApartmentByIdQuery(Guid Id) : IQuery<ApartmentResponse>
    {
    }
}
