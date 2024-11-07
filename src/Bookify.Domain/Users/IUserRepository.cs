using Bookify.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Users
{
    public interface IUserRepository
    {
        /// <summary>
        /// Получить пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователяа</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public void Add(User user);
    }
}
