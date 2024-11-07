using Bookify.Aplication.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookify.Aplication.Apartments
{
    public record CreateApartmentCommand(
        string Name,  string Description, string Country,
        string State, string ZipCode, string City,
        string Street, decimal PriceAmount, decimal CleaningFeeAmount,
        string Code, List<int> Amenities)
        : ICommand<Guid>;

}
