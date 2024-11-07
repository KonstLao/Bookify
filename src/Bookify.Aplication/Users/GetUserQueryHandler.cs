using Bookify.Aplication.Abstractions.Data;
using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Aplication.Bookings;
using Bookify.Domain.Abstractions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Users
{
    internal sealed class GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetUserQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            using var conection = sqlConnectionFactory.CreateConnection();
            var sql = $"""
                      SELECT
                      
                      Id as {nameof(UserResponse.Id)},
                      FirstName as {nameof(UserResponse.FirstName)},
                      LastName as {nameof(UserResponse.SecondName)},
                      Email as {nameof(UserResponse.Email)},
                      UserName as {nameof(UserResponse.UserName)}
                      
                      FROM Users WHERE Id = @UserId
                      
                      """;
            var result = await conection.QueryFirstOrDefaultAsync<UserResponse>(sql, new { request.UserId });
            return result;
        }
    }
}
