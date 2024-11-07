using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Users
{
    public class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.CreateUser(
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                new UserEmail(request.Email),
                new UserName(request.UserName));
            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(user.Id);
        }
    }
}
