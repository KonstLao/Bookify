using Bogus;
using Bogus.DataSets;
using Bookify.Aplication.Abstractions.Data;
using Bookify.Aplication.Apartments;
using Bookify.Aplication.Bookings;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;
using Bookify.Domain.Shared;
using Bookify.Domain.Users;
using Bookify.Infrastructure;
using Dapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Currency = Bookify.Domain.Shared.Currency;

namespace Bookify.Api.Extensions
{
    public static class SeedDataExtensions 
    {

        public static async Task SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using var connection = sqlConnectionFactory.CreateConnection();
            var faker = new Faker("ru");
            var rowCount = await connection.ExecuteScalarAsync<int>("Select COUNT(*) from Apartments");
            var apartments = new List<dynamic>();
            var users = new List<dynamic>();
            var bookings = new List<dynamic>();
            var amenities = new Dictionary<Guid, List<Amenity>>();
            //генерация апартамментов
            if (rowCount < 100)
            {

                for (var i = rowCount; i < 100; i++)
                {
                    var currency = Currency.RandomCurrency();
                    var amenitiesList = Enum.GetValues<Amenity>()
                                            .Where(e => faker.Random.Int(0, 2) == 0).ToList(); // 33%
                    var finalAmenities = JsonConvert.SerializeObject(amenitiesList.Select(e => e.ToString()).ToList());

                    var apartment = new
                    {
                        Id = Guid.NewGuid(),
                        Name = faker.Lorem.Word(),
                        Description = DescriptionSubstring.RandomDescription(faker.Lorem.Text(), 399),
                        Address_Country = faker.Address.Country(),
                        Address_State = faker.Address.State(),
                        Address_ZipCode = faker.Address.ZipCode(),
                        Address_City = faker.Address.City(),
                        Address_Street = faker.Address.StreetAddress(),
                        Price_Amount = faker.Random.Decimal(50, 1000),
                        Price_Currency = currency,
                        CleaningFee_Amount = faker.Random.Decimal(50, 1000),
                        CleaningFee_Currency = currency,
                        LastBookedOnUtc = faker.Date.Past(5, DateTime.UtcNow),
                        Amenities = finalAmenities,
                    };
                    const string sql = """
                                    INSERT INTO Apartments
                                    (Id, Name, Description, Address_Country, Address_State, Address_ZipCode, Address_City, Address_Street,
                                    Price_Amount, Price_Currency, CleaningFee_Amount, CleaningFee_Currency, LastBookedOnUtc, Amenities)
                                    VALUES(@Id, @Name, @Description, @Address_Country, @Address_State, @Address_ZipCode, @Address_City, @Address_Street,
                                    @Price_Amount, @Price_Currency, @CleaningFee_Amount, @CleaningFee_Currency, @LastBookedOnUtc, @Amenities)
                                    """;
                    await connection.ExecuteAsync(sql, apartment);
                    apartments.Add(apartment);
                    amenities.Add(apartment.Id, amenitiesList);
                }

            }

            //генерация пользователей
            rowCount = await connection.ExecuteScalarAsync<int>("Select COUNT(*) from Users");
            if (rowCount < 200)
            {
                for (var i = rowCount; i < 200; i++)
                {
                    var user = new
                    {
                        Id = Guid.NewGuid(),
                        FirstName = faker.Name.FirstName(),
                        LastName = faker.Name.LastName(),
                        Email = faker.Internet.Email(),
                        UserName = faker.Internet.UserName()
                    };
                    const string sql = """
                                    INSERT INTO Users
                                    (Id, FirstName, LastName, Email, UserName)
                                    VALUES(@Id, @FirstName, @LastName, @Email, @UserName)
                                    """;
                    await connection.ExecuteAsync(sql, user);
                    users.Add(user);
                }

            }

            //генерация букингов
            rowCount = await connection.ExecuteScalarAsync<int>("Select COUNT(*) from Bookings");
            if (rowCount < 5000)
            {
                var dateRangeGenerator = new DateRangeGenerator();
                //var bookingStatusGenerator = new BookingStatusGenerator();
                for (var i = rowCount; i < 5000; i++)
                {
                    var bookingStatusGenerator = new BookingStatusGenerator();
                    dateRangeGenerator.GenerateDateRange();
                    var apartment = apartments[faker.Random.Int(0, apartments.Count - 1)];
                    var user = users[faker.Random.Int(0, users.Count - 1)];
                    bookingStatusGenerator.Generate(dateRangeGenerator.StartDate, dateRangeGenerator.EndDate);
                    var amenity = PricingService.GetAmenitiesUpCharge(amenities [apartment.Id], apartment.Price_Amount);
                    var totalPrice = PricingService.GetTotalPrice(apartment.Price_Amount, apartment.CleaningFee_Amount, amenity);
                    var currency = apartment.Price_Currency;
                    
                    var booking = new
                    {
                        Id = Guid.NewGuid(),
                        ApartmentId = apartment.Id,
                        UserId = user.Id,
                        DateRange_Start = dateRangeGenerator.StartDate.ToDateTime(TimeOnly.MinValue),
                        DateRange_End = dateRangeGenerator.EndDate.ToDateTime(TimeOnly.MinValue),
                        DateRange_LengthInDays = dateRangeGenerator.LenghthIndays,
                        PriceForPeriod_Amount = apartment.Price_Amount,
                        PriceForPeriod_Currency = currency,
                        CleaningFee_Amount = apartment.CleaningFee_Amount,
                        CleaningFee_Currency = currency,
                        AmenitiesUpCharge_Amount = amenity,
                        AmenitiesUpCharge_Currency = currency,
                        TotalPrice_Amount = totalPrice,
                        TotalPrice_Currency = currency,
                        Status = bookingStatusGenerator.status,
                        CreatedOnUtc = bookingStatusGenerator.createdOnUtc,
                        ConfirmedOnUtc = bookingStatusGenerator.confirmedOnUtc,
                        RejectedOnUtc = bookingStatusGenerator.rejectedOnUtc,
                        CompletedOnUtc = bookingStatusGenerator.completedOnUc,
                        CancelledOnUtc = bookingStatusGenerator.cancelledOnUtc
                    };
                    bookings.Add(booking);
                    const string sql = """
                                    INSERT INTO Bookings
                                    (Id, ApartmentId, UserId, DateRange_Start, DateRange_End, DateRange_LengthInDays, PriceForPeriod_Amount, PriceForPeriod_Currency,
                                    CleaningFee_Amount, CleaningFee_Currency, AmenitiesUpCharge_Amount, AmenitiesUpCharge_Currency, TotalPrice_Amount,
                                    TotalPrice_Currency, Status, CreatedOnUtc, ConfirmedOnUtc, RejectedOnUtc, CompletedOnUtc, CancelledOnUtc)
                                    VALUES(@Id, @ApartmentId, @UserId, @DateRange_Start, @DateRange_End, @DateRange_LengthInDays, @PriceForPeriod_Amount, @PriceForPeriod_Currency,
                                    @CleaningFee_Amount, @CleaningFee_Currency, @AmenitiesUpCharge_Amount, @AmenitiesUpCharge_Currency, @TotalPrice_Amount,
                                    @TotalPrice_Currency, @Status, @CreatedOnUtc, @ConfirmedOnUtc, @RejectedOnUtc, @CompletedOnUtc, @CancelledOnUtc)
                                    """;
                    await connection.ExecuteAsync(sql, booking);

                }

            }
            //генерация отзывов(review)

            rowCount = await connection.ExecuteScalarAsync<int>("Select COUNT(*) from Reviews");
            var endedBookings = new List<dynamic>();
            foreach(var booking in bookings)
            {
                if (booking.Status == 5)
                {
                    endedBookings.Add(booking);
                }
            }
            if (rowCount < 100)
            {
                for (var i = rowCount; i < 100; i++)
                {
                    var booking = endedBookings[faker.Random.Int(0, endedBookings.Count - 1)];
                    endedBookings.Remove(booking);
                    var review = new
                    {
                        Id = Guid.NewGuid(),
                        ApartmentId = booking.ApartmentId,
                        UserId = booking.UserId,
                        BookingId = booking.Id,
                        Rating = faker.Random.Int(1, 10),
                        Comment = DescriptionSubstring.RandomDescription(faker.Lorem.Text(),300),
                        CreatedOnUtc = booking.CompletedOnUtc,

                    };
                    const string sql = """
                                    INSERT INTO Reviews
                                    (Id, ApartmentId, UserId, BookingId, Rating, Comment, CreatedOnUtc)
                                    VALUES (@Id, @ApartmentId, @UserId, @BookingId, @Rating, @Comment, @CreatedOnUtc)
                                    """;
                    await connection.ExecuteAsync(sql, review);
                    
                }

            }
        }

    }
}

