using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings
{
	/// <summary>
	/// Статус бронирования
	/// </summary>
	public enum BookingStatus
	{
		/// <summary>
		/// Апартамент зарезервирован
		/// </summary>
		Reserved = 1,

		/// <summary>
		/// Бронирование успешно подтверждено сервисом
		/// </summary>
		Confirmed = 2,

		/// <summary>
		/// Бронирование отклонено сервисом
		/// </summary>
		Rejected = 3,

		/// <summary>
		/// Бронирование отменено клиентом
		/// </summary>
		Cancelled = 4,

		/// <summary>
		/// Бронирование завершено
		/// </summary>
		Completed = 5
	}
	//public class BookingStatus1
 //   {
 //       public Status Status { get; private set; }
 //       public Reason? Reason {  get; private set; }
 //       public BookingStatus1() 
 //       {
 //           Reason = null;
 //           Status = Status.Reserved;
 //       }

 //       public Result<BookingStatus> Confirm()
 //       {
 //           if(Status == Status.Reserved)
 //           {
 //               Status = Status.Confirmed;
 //               return Result.Success(this);
 //           }
 //           else
 //           {
 //               return Result.Failure<BookingStatus>(BookingErrors.BookingStatusInvalid);
 //           }
 
 //       }
 //       public Result<BookingStatus> Rejecte(Reason reason)
 //       {

 //           if (Status == Status.Reserved || Status == Status.Confirmed)
 //           {
 //               Status = Status.Rejected;
 //               Reason = reason;
 //               return Result.Success(this);
 //           }
 //           else
 //           {
 //               return Result.Failure<BookingStatus>(BookingErrors.BookingStatusInvalid);
 //           }
 //       }
 //       public Result<BookingStatus> Complete()
 //       {
 //           if (Status == Status.Confirmed)
 //           {
 //               Status = Status.Completed;
 //               return Result.Success(this);
 //           }
 //           else
 //           {
 //               return Result.Failure<BookingStatus>(BookingErrors.BookingStatusInvalid);
 //           }

 //       }
 //   }

    public enum Status
    {
        Reserved,
        Confirmed,
        Rejected,
        Completed
    }

    public enum Reason
    {
        Castomer,
        System,
    }
}
