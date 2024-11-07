using Bookify.Aplication.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookify.Aplication.Users
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string UserName) : ICommand<Guid>;

}
