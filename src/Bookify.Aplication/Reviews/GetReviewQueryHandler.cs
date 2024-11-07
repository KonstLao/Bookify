using Bookify.Aplication.Abstractions.Data;
using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Reviews;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bookify.Aplication.Reviews
{
    public class GetReviewQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetReviewQuery, ReviewResponse>
    {
        public async Task<Result<ReviewResponse>> Handle(GetReviewQuery request, CancellationToken cancellationToken)
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
                      
                      FROM reviews WHERE Id = @Id
                      
                      """;
            var result = await conection.QueryFirstOrDefaultAsync<ReviewResponse>(sql, new { request.Id });
            if (result == null)
            {
                return Result.Failure<ReviewResponse>(new Error("Review", "Review not found"));
            }

            return result;
        }
    }
}
