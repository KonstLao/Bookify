using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews
{
    public static class ReviewErrors
    {
        public static readonly Error RatingInvalid = new("Rating.Invalid", "The rating is invalid");

        public static readonly Error NotEligible = new(
	        "Review.NotEligible",
	        "The review is not eligible because the booking is not yet completed");
	}
}
