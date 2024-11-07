using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Aplication.Abstractions.Data;
using Bookify.Aplication.Bookings;
using Dapper;
using Bookify.Domain.Bookings;
using Bookify.Domain.Apartments;

namespace Bookify.Aplication.Apartments
{

    internal sealed class SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
       : IQueryHandler<SearchApartmentsQuery, List<ApartmentResponse>>
    {
        private static readonly int[] ActiveBookingStatuses =
       {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed
        };

        public async Task<Result<List<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
        {

            const string sql = $"""
            SELECT
                a.Id AS Id,
                a.Name AS {nameof(ApartmentResponse.Name)},
                a.Description AS {nameof(ApartmentResponse.Description)},
                a.Price_Amount AS {nameof(ApartmentResponse.Price)},
                a.Price_Currency AS {nameof(ApartmentResponse.Currency)},
                a.LastBookedOnUtc AS {nameof(ApartmentResponse.LastBookedonUTC)},
                a.Amenities as {nameof(ApartmentResponse.Amenities)},
                a.Address_Country AS {nameof(ApartmentResponse.Address.Country)},
                a.Address_State AS {nameof(ApartmentResponse.Address.State)},
                a.Address_ZipCode AS {nameof(ApartmentResponse.Address.ZipCode)},
                a.Address_City AS {nameof(ApartmentResponse.Address.City)},
                a.Address_Street AS {nameof(ApartmentResponse.Address.Street)}

            FROM apartments AS a
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM bookings AS b
                WHERE
                    b.apartmentId = a.Id AND
                    b.DateRange_Start <= @StartDate AND
                    b.DateRange_End >= @EndDate AND
                    b.Status IN @ActiveBookingStatuses
                
            )
            """;

            using var conection = sqlConnectionFactory.CreateConnection();
            var result = (await conection.
	            QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse> (sql, 
		            (apartment, addresss) =>
		            {
			            apartment.Address = addresss;
                        apartment.AmenitiesList = AmenitiesEx.ConvertToAmenitiesList(apartment.Amenities);
                        return apartment;
		            }, 
		            new
		            {
			            request.StartDate,
			            request.EndDate,
			            ActiveBookingStatuses
					},
		            splitOn: nameof(ApartmentResponse.Address.Country))).ToList();

            return result;





            //var apartmentResponses = result.Select(r => new ApartmentResponse
            //{
            // Id = r.Id,
            // Name = r.Name,
            // Description = r.Description,
            // Price = r.Price,
            // Currency = r.Currency,
            // Address = new AddressResponse
            // {
            //  Country = r.Country,
            //  State = r.State,
            //  ZipCode = r.ZipCode,
            //  City = r.City,
            //  Street = r.Street
            // }
            //}).ToList();


            //using var conection = sqlConnectionFactory.CreateConnection();
            //var result = (await conection.QueryAsync<ApartmentResult>( sql, new {request.StartDate, request.EndDate, ActiveBookingStatuses } )).ToList();
            //var apartmentResponses =  result.Select(r => new ApartmentResponse
            //{
            //    Id = r.Id,
            //    Name = r.Name,
            //    Description = r.Description,
            //    Price = r.Price,
            //    Currency = r.Currency,
            //    Address = new AddressResponse
            //    {
            //        Country = r.Country,
            //        State = r.State,
            //        ZipCode = r.ZipCode,
            //        City = r.City,
            //        Street = r.Street
            //    }
            //}).ToList();
            //return apartmentResponses;
        }
    }

}
