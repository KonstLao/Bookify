using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bookify.Aplication.Bookings.GetBookings;

namespace Bookify.Aplication.Bookings
{
    /// <summary>
    /// Представляет информацию о бронировании.
    /// </summary>
    public sealed class BookingResponse
    {
        /// <summary>
        /// Идентификатор бронирования
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// Идентификатор квартиры
        /// </summary>
        public Guid ApartmentId { get; init; }

        /// <summary>
        /// Статус бронирования
        /// </summary>
        public int Status { get; init; }

        /// <summary>
        /// Цена
        /// </summary>
        public MoneyResponse Price { get; set; }



    }
}


