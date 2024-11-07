using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Apartments
{
    public class DeleteApartmentCommandHandler(
        IUnitOfWork unitOfWork,
        IApartmentRepository apartmentRepository,
        IBookingRepository bookingRepository) : ICommandHandler<DeleteApartmentCommand>
    {
        public async Task<Result> Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            var bookings = await bookingRepository.GetBookingsByApartmentIdAsync(request.Id);

            if (bookings.Any())
            {

                return Result.Failure(new Error("ApartmentDeleteError","Невозможно удалить квартиру, так как у нее есть бронирования."));
            }
            var result = apartmentRepository.Delete(request.Id);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
