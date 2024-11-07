using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookify.Aplication.Reviews
{
    public record CreateReviewCommand(Guid Id, int Rating, string Comment) : ICommand<Guid>
    {
    }
}
