using Bookify.Aplication.Abstractions.Data;
using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Aplication.Bookings;
using Bookify.Aplication.Users;
using Bookify.Domain.Abstractions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Reviews
{
    internal sealed class GetReviewsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetReviewsQuery, List<ReviewResponse>>
    {
        public async Task<Result<List<ReviewResponse>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            using var conection = sqlConnectionFactory.CreateConnection();
            var sql = $"""
                      SELECT
                      
                      Id as {nameof(ReviewResponse.Id)},
                      ApartmentId as {nameof(ReviewResponse.ApartmentId)},
                      UserId as {nameof(ReviewResponse.UserId)},
                      BookingId as {nameof(ReviewResponse.BookingId)},
                      Rating as {nameof(ReviewResponse.Rating)},
                      Comment as {nameof(ReviewResponse.Comment)},
                      CreatedOnUtc as {nameof(ReviewResponse.CreatedOnUtc)}
                      
                      FROM reviews WHERE ApartmentId = @ApartmentId
                      
                      """;
            var result = await conection.QueryAsync<ReviewResponse>(sql, new { request.ApartmentId });
            return result.ToList();
        }
    }
}
