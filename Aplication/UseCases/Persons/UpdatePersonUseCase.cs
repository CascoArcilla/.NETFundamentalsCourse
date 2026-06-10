using Aplication.DTOs.Persons;
using Domain;
using Domain.Abstractions;

namespace Aplication.UseCases.Persons
{
    public class UpdatePersonUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;

        public UpdatePersonUseCase(IRepository<PersonEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PersonEntity> ExecuteAsync(UpdatePersonDto dto)
        {
            var person = await _repository.GetByIdAsync(dto.Id);
            if (person == null) throw new InvalidOperationException("La persona no existe.");

            person.UpdatePersonInfo(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.PhoneNumber
                );

            await _repository.UpdateAsync(person);
            await _repository.SaveChangesAsync();

            return person;
        }
    }
}
