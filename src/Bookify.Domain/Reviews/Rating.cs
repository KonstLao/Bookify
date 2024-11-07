using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews; 

public record Rating
{
    public int Value { get; init; }
    private Rating(int value)
    {
        Value = value;
    }
    public static Result<Rating> Create(int value)
    {
        if (value > 0 && value <= 5)
        {
            return Result.Success(new Rating(value));
        }
        else
        {
            return Result.Failure<Rating>(ReviewErrors.RatingInvalid);
        }
    }
}
