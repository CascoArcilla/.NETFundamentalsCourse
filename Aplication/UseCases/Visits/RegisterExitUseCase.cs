using Aplication.DTOs.Visits;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.UseCases.Visits
{
    public class RegisterExitUseCase
    {
        private readonly IRepository<VisitEntity, Guid> _repository;
        private readonly IVisitRepository<VisitEntity> _visitRepository;

        public RegisterExitUseCase(IRepository<VisitEntity, Guid> repository, IVisitRepository<VisitEntity> visitRepository)
        {
            _repository = repository;
            _visitRepository = visitRepository;
        }

        public async Task<VisitEntity> ExecuteAsync(RegisterExitDto dto)
        {
            VisitEntity? visit;

            if (dto.VisitId.HasValue)
            {
                visit = await _repository.GetByIdAsync(dto.VisitId.Value);
                if (visit == null)
                    throw new InvalidOperationException($"Visita no encontrada por Id: {dto.VisitId.Value}");
            }

            else if (!string.IsNullOrWhiteSpace(dto.Code))
            {
                visit = await _visitRepository.GetVisitActiveByPersonCodeAsync(dto.Code);
                if (visit == null)
                    throw new InvalidOperationException($"Visita no encontrada por Codigo: {dto.Code}");
            }

            else
            {
                throw new InvalidOperationException("Debe proporcionar un Id de visita o un código de persona");
            }

            visit.RegisterExitTime(dto.ExitTime);

            await _repository.UpdateAsync(visit);
            await _repository.SaveChangesAsync();

            return visit;
        }
    }
}
