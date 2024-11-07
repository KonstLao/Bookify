using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Reviews
{
    public record GetReviewQuery(Guid Id) : IQuery<ReviewResponse>
    {
    }
}
