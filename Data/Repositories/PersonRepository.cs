using Data.Persisten;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    // Esta es la clase que hace el "¿como?"
    public class PersonRepository : IRepository<PersonEntity, Guid>, ICodeRepository<PersonEntity>
    {
        private readonly AplicationDbContext _context;

        public PersonRepository(AplicationDbContext context)
        {
            _context = context;
        }

        // Implementacion de IRepository

        public async Task AddAsync(PersonEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.Persons.AddAsync(entity);
        }

        public Task DeleteAsync(PersonEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Persons.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<PersonEntity>> GetAllAsync()
        {
            return await _context.Persons
                .AsNoTracking() // Mejor rendimiento para consultas de solo lectura, evita el seguimiento de cambios
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToListAsync();
        }

        public async Task<PersonEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Persons
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(PersonEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Persons.Update(entity);
            return Task.CompletedTask;
        }

        // Implementacion de ICodeRepository

        public async Task<bool> ExistWithCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("El codigo no puede estar vacio");
            var normalizedCode = code.ToUpperInvariant();
            return await _context.Persons.AnyAsync(p => p.Code == normalizedCode);
        }

        public async Task<PersonEntity?> GetByCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("El codigo no puede estar vacio");
            var normalizedCode = code.ToUpperInvariant();
            return await _context.Persons.FirstOrDefaultAsync(p => p.Code == normalizedCode);
        }
    }
}
