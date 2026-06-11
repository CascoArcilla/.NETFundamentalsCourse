using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions
{
    public interface IVisitRepository<TEntity> where TEntity : class
    {
        Task<bool> HasActivateVisitAsync(Guid personId);
        Task<TEntity> GetVisitActiveByPersonCodeAsync(string code);
        Task<IEnumerable<TEntity>> GetVisitsByPersonIdAsync(Guid personId);
        Task<IEnumerable<TEntity>> GetActivateVisitsAsync();
    }
}
