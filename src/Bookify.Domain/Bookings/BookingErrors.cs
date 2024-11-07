using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings
{
    public static class BookingErrors
    {
        public static readonly Error BookingStatusInvalid = new("Booking.Status.Invalid", "The status of booking is invalid");

        /// <summary>
        /// Бронирование не зарезервировано
        /// </summary>
        public static readonly Error NotReserved = new Error(
	        "Booking.NotReserved",
	        "The booking is not pending");

        public static readonly Error RejectErr = new Error(
            "Booking.NotReserved or Booking.NotConfirmed",
            "Error while try to reject");

        public static readonly Error CancellErr = new Error(
            "Booking.NotReserved or Booking.NotConfirmed",
            "Error while try to cancell");
        public static readonly Error NotConfirmed = new Error(
            "Booking.NotConfirmed",
            "The booking is not confirmed");

        public static readonly Error DateRangeErr = new Error(
            "Booking.DateRange",
            "StartDate bigger then EndDate");
    }

}
