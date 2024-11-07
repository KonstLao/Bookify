using Bookify.Aplication.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Apartments
{
    public sealed record DeleteApartmentCommand(Guid Id) : ICommand
    {
    }
}
