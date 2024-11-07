using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Repositories
{
    internal abstract class Repository<T> where T : Entity
    {
        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<T>()
                .SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        public void Add(T entity)
        {
            DbContext.Add(entity);
        }
        public void Update(T entity)
        {
            DbContext.Update(entity);
        }
        public bool Delete(Guid id)
        {
            // Находим сущность по ID
            T entity =  DbContext.Find<T>(id);

            // Если сущность найдена, удаляем ее
            if (entity != null)
            {
                DbContext.Remove(entity);
                return true; // Возвращаем true, если удаление произошло успешно
            }

            return false; // Возвращаем false, если сущность не найдена
        }
    }

}
