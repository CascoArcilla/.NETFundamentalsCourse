using Domain;
using Domain.Abstractions;

namespace Aplication.UseCases.Persons
{
    public class GetPersonByCodeUseCase
    {
        private readonly ICodeRepository<PersonEntity> _repository;

        public GetPersonByCodeUseCase(ICodeRepository<PersonEntity> repository)
        {
            _repository = repository;
        }

        public async Task<PersonEntity?> ExecuteAsync(string code)
        {
            var person = await _repository.GetByCodeAsync(code);
            if (person == null) throw new InvalidOperationException($"No se encontró persona con el código: {code}");
            return person;
        }
    }
}