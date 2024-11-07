using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartments
{
    public interface IApartmentRepository
    {
        /// <summary>
        /// Получить апартмет по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор апартмента</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public void Add(Apartment apartment);
        public bool Delete(Guid id);

    }
}
