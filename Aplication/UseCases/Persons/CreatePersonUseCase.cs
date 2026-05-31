using Aplication.DTOs.Persons;
using Domain;
using Domain.Abstractions;

namespace Aplication.UseCases.Persons
{
    public class CreatePersonUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _personRepository;
        private readonly ICodeRepository<PersonEntity> _codeRepository;

        public CreatePersonUseCase(IRepository<PersonEntity, Guid> personRepository, ICodeRepository<PersonEntity> codeRepository)
        {
            _personRepository = personRepository;
            _codeRepository = codeRepository;
        }

        public async Task<PersonEntity> ExecuteAsync(CreatePersonDto dtoPerson)
        {
            if (await _codeRepository.ExistWithCodeAsync(dtoPerson.Code))
                throw new InvalidOperationException("El código ya existe en el sistema.");

            PersonEntity person = new PersonEntity(
                dtoPerson.Code,
                dtoPerson.FirstName,
                dtoPerson.LastName,
                dtoPerson.Email,
                dtoPerson.PhoneNumber
                );

            await _personRepository.AddAsync(person);
            await _personRepository.SaveChangesAsync();

            return person;
        }
    }
}
