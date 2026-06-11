using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.UseCases.Visits
{
    public class GetVisitsUseCase
    {
        private readonly IRepository<VisitEntity, Guid> _visitRepository;

        public GetVisitsUseCase(IRepository<VisitEntity, Guid> visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<IEnumerable<VisitEntity>> ExecuteAsync()
        {
            return await _visitRepository.GetAllAsync();
        }
    }
}
