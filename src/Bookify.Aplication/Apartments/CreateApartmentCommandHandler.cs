using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Reviews;
using Bookify.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Apartments
{
    public sealed class CreateApartmentCommandHandler(
            IApartmentRepository apartmentRepository,
            IUnitOfWork unitOfWork)
            : ICommandHandler<CreateApartmentCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateApartmentCommand request, CancellationToken cancellationToken)
        {
            // ToDo добавить проверку amenities на отсутствие повторов одного и тогоже удобства
            var apartment = Apartment.CreateApartment(
                new Name(request.Name),
                new Description(request.Description),
                new Address(request.Country, request.State, request.ZipCode, request.City, request.Street),
                new Money(request.PriceAmount, Currency.FromCode(request.Code)),
                new Money(request.CleaningFeeAmount, Currency.FromCode(request.Code)),
                AmenitiesEx.ToAmenityList(request.Amenities));
            apartmentRepository.Add(apartment.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(apartment.Value.Id);
        }
    }
}
