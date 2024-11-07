using Bookify.Aplication.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Reviews
{
    public class CreateReviewCommandHandler(
        IUnitOfWork unitOfWork,
        IBookingRepository bookingRepository, 
        IReviewRepository reviewRepository) : ICommandHandler<CreateReviewCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.Id, cancellationToken);
            var review = Review.CreateReview(
                booking,
                Rating.Create(request.Rating).Value,
                new Comment(request.Comment),
                DateTime.Now);
            reviewRepository.Add(review.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(review.Value.Id);
        }
    }
}
