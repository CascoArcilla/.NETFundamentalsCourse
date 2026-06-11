using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.UseCases.Visits
{
    public class GetActiveVisitsUseCase
    {
        private readonly IVisitRepository<VisitEntity> _visitRepository;

        public GetActiveVisitsUseCase(IVisitRepository<VisitEntity> visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public Task<IEnumerable<VisitEntity>> ExecuteAsync()
        {
            return _visitRepository.GetActivateVisitsAsync();
        }
    }
}