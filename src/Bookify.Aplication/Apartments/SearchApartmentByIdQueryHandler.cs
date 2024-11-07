using Bookify.Aplication.Abstractions.Data;
using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Apartments
{
    internal sealed class SearchApartmentByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
           : IQueryHandler<SearchApartmentByIdQuery, ApartmentResponse>
    {

        public async Task<Result<ApartmentResponse>> Handle(SearchApartmentByIdQuery request, CancellationToken cancellationToken)
        {

            const string sql = $"""
            SELECT
                a.Id AS {nameof(ApartmentResponse.Id)},
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
            WHERE 
            (
                Id = @Id 
            )
            """;

            using var conection = sqlConnectionFactory.CreateConnection();
            var result = (await conection.
                QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(sql,
                    (apartment, addresss) =>
                    {
                        apartment.Address = addresss;
                        apartment.AmenitiesList = AmenitiesEx.ConvertToAmenitiesList(apartment.Amenities);
                        return apartment;
                    },
                    new
                    {
                        request.Id
                    },
                    splitOn: nameof(ApartmentResponse.Address.Country))).ToList().FirstOrDefault();

            return result;

        }
    }
}
