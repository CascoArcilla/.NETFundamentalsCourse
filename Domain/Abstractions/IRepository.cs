namespace Domain.Abstractions
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        // Task es usado para operaciones asincrónicas
        Task<TEntity?> GetByIdAsync(TId id);
        // IEnumerable se usa para devolver una colección (listado) de entidades
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
