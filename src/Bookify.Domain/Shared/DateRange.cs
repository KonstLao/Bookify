using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;

namespace Bookify.Domain.Shared;

public record DateRange
{
	public DateOnly Start { get; init; }
	public DateOnly End { get; init; }

	public int LengthInDays { get; private set; }
	private DateRange() { }
    private DateRange (DateOnly startDate, DateOnly endDate)
    {
	    LengthInDays = endDate.DayNumber - startDate.DayNumber;
		Start = startDate;
		End = endDate;
    }

	public static Result<DateRange> GetRange(DateOnly startDate, DateOnly endDate)
	{
        if (startDate > endDate) 
		{
			return Result.Failure<DateRange>(BookingErrors.DateRangeErr);
		}
        return Result.Success(new DateRange(startDate, endDate));
    }

    /// <summary>
    /// Проверка, пересекается ли текущий диапазон с другим диапазоном
    /// </summary>
    /// <param name="other"></param>
    /// <returns>bool</returns>
    public bool IsDisjointWith(DateRange other)
    {
        return End < other.Start || Start > other.End;
    }


}
