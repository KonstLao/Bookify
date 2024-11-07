using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews.ReviewEvents;

namespace Bookify.Domain.Reviews;

public sealed class Review : Entity
{
    private Review() { }
    private Review(Guid reviewId, Guid apartmentId, Guid userId, Guid bookingId, Rating rating, Comment comment, DateTime createdOnUtc) : base(reviewId)
    {
	    ApartmentId = apartmentId;
        UserId = userId;
        BookingId = bookingId;
        Rating = rating;
        Comment = comment;
        CreatedOnUtc = createdOnUtc;
        Id = reviewId;
    }
    public Guid ApartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BookingId { get; private set; }
    public Rating Rating { get; private set; }
    public Comment Comment { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public static Result<Review> CreateReview(Booking booking, Rating rating, Comment comment, DateTime createdOnUtc)
    {

	    if (booking.Status != BookingStatus.Completed)
	    {
		    return Result.Failure<Review>(ReviewErrors.NotEligible);
	    }
        Guid id = Guid.NewGuid();
	    var review = new Review(
            id,
		    booking.ApartmentId,
		    booking.UserId,
			booking.Id,
		    rating,
		    comment,
		    createdOnUtc);
        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(id));
	    return Result.Success(review);

    }
}