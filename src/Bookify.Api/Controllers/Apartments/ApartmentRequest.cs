namespace Bookify.Api.Controllers.Apartments
{
    public sealed record ApartmentRequest(
        string Name, string Description, string Country,
        string State, string ZipCode, string City,
        string Street, decimal PriceAmount, decimal CleaningFeeAmount,
        string Code, List<int> Amenities)
    {
    }
}
