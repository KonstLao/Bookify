using Bookify.Aplication.Abstractions.Data;
using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Aplication.Apartments;
using Bookify.Domain.Abstractions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Bookings.GetBookings
{
    internal sealed class GetAllBookingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetAllBookingsQuery, List<BookingResponse>>
    {

        public async Task<Result<List<BookingResponse>>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            using var conection = sqlConnectionFactory.CreateConnection();
            const string sql = $"""
            SELECT
                b.Id AS {nameof(BookingResponse.Id)},
                b.UserId AS {nameof(BookingResponse.UserId)},
                b.ApartmentId AS {nameof(BookingResponse.ApartmentId)},
                b.Status AS {nameof(BookingResponse.Status)},
                b.TotalPrice_Amount AS {nameof(BookingResponse.Price.Amount)},
                b.TotalPrice_Currency AS {nameof(BookingResponse.Price.Currency)}

            FROM bookings AS b
            WHERE 
            (
                UserId = @UserId    
            )
            """;

            var result = (await conection.QueryAsync<BookingResponse, MoneyResponse, BookingResponse>(sql,
                (booking, money)=>
                {
                    booking.Price = money;
                    return booking;
                },
                new { request.UserId },
                splitOn: nameof(BookingResponse.Price.Amount)
                )).ToList();
            //result = result.ToList();
            //return Result.Success(result);
            return result;
        }
    }
}
