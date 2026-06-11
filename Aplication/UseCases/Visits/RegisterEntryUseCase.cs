using Aplication.DTOs.Visits;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.UseCases.Visits
{
    public class RegisterEntryUseCase
    {
        private readonly ICodeRepository<PersonEntity> _codeRepository;
        private readonly IVisitRepository<VisitEntity> _visitRepository;
        private readonly IRepository<VisitEntity, Guid> _repository;

        public RegisterEntryUseCase(IVisitRepository<VisitEntity> visitRepository,
            ICodeRepository<PersonEntity> codeRepository,
            IRepository<VisitEntity, Guid> repository)
        {
            _visitRepository = visitRepository;
            _codeRepository = codeRepository;
            _repository = repository;
        }

        public async Task<VisitEntity> ExecuteAsync(RegisterEntryDto dto)
        {
            Guid personId;

            if (dto.PersonId.HasValue)
            {
                personId = dto.PersonId.Value;
            }
            else if (!string.IsNullOrWhiteSpace(dto.Code))
            {
                var person = await _codeRepository.GetByCodeAsync(dto.Code);

                if (person == null)
                    throw new InvalidOperationException("El codigo pasado es invalido");

                personId = person.Id;
            }
            else
            {
                throw new ArgumentException("No se proporciono Id o Codigo de persona");
            }

            if (await _visitRepository.HasActivateVisitAsync(personId))
                throw new InvalidOperationException("Ya existe una visita activa para esta persona");

            var visit = new VisitEntity(personId, dto.EntryTime);

            await _repository.AddAsync(visit);
            await _repository.SaveChangesAsync();

            return await _repository.GetByIdAsync(visit.Id) ??
                throw new InvalidOperationException("Error al obtener visita creada");
        }
    }
}
