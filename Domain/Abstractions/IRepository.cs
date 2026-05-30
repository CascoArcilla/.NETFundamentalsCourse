using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        // Task es usado para operaciones asincrónicas
        Task<TEntity?> GetByIdAync(TId id);
        // IEnumerable se usa para devolver una colección (listado) de entidades
        Task<IEnumerable<TEntity>> GetAllAync(TId id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
        Task<int> SaveChangesAsync();
    }
}
