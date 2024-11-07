using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Users
{
    public record GetUserQuery(Guid UserId) : IQuery<UserResponse>
    {
    }
}
