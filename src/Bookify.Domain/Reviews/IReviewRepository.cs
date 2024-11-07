using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Reviews
{
    public interface IReviewRepository
    {
        /// <summary>
        /// Получить пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователяа</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// добавление нового отзыва в БД
        /// </summary>
        /// <param name="review">отзыв</param>
        public void Add(Review review);
    }
}
