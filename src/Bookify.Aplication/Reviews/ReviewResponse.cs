using Bookify.Domain.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Reviews
{
    /// <summary>
    /// Отзыв
    /// </summary>
    public record ReviewResponse
    {
        /// <summary>
        /// Идентификатор Бронирования
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор квартиры
        /// </summary>
        public Guid ApartmentId { get; private set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Идентификатор бронирования
        /// </summary>
        public Guid BookingId { get; private set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int Rating { get; private set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string? Comment { get; private set; }

        /// <summary>
        /// Дата создания бронирования
        /// </summary>
        public DateTime CreatedOnUtc { get; private set; }
    }
}
