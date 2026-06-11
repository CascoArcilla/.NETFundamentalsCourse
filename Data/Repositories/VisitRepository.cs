using Azure.Identity;
using Data.Persisten;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class VisitRepository : IRepository<VisitEntity, Guid>, IVisitRepository<VisitEntity>
    {
        private readonly AplicationDbContext _context;

        public VisitRepository(AplicationDbContext context)
        {
            _context = context;
        }

        // Implementacion de IRepository
        public async Task AddAsync(VisitEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.Visits.AddAsync(entity);
        }

        public Task DeleteAsync(VisitEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Visits.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<VisitEntity>> GetAllAsync()
        {
            return await _context.Visits
                .AsNoTracking()
                .OrderBy(v => v.PersonId)
                .ToListAsync();
        }

        public async Task<VisitEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Visits
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(VisitEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Visits.Update(entity);
            return Task.CompletedTask;
        }

        // Implementacion de IVisitRepository

        public async Task<bool> HasActivateVisitAsync(Guid personId)
        {
            if (personId == Guid.Empty) throw new ArgumentNullException("El Id de persona no puede estar vacio");
            return await _context.Visits.AnyAsync(v => v.PersonId == personId);
        }

        public async Task<VisitEntity?> GetVisitActiveByPersonCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("El codigo no puede estar vacio");
            var normalizedCode = code.ToUpperInvariant();
            return await _context.Visits.FirstOrDefaultAsync(v => v.Person.Code == normalizedCode);
        }

        public async Task<IEnumerable<VisitEntity>> GetVisitsByPersonIdAsync(Guid personId)
        {
            if (personId == Guid.Empty) throw new ArgumentNullException("El Id de persona no puede estar vacio");
            return await _context.Visits.AsNoTracking().Where(v => v.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<VisitEntity>> GetActivateVisitsAsync()
        {
            return await _context.Visits.AsNoTracking().Where(v => v.isActive).ToListAsync();
        }
    }
}
