using Domain;
using Domain.Abstractions;

namespace Aplication.UseCases.Persons
{
    public class GetPersonByIdUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;

        public GetPersonByIdUseCase(IRepository<PersonEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PersonEntity?> ExecuteAsync(Guid id)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null) throw new InvalidOperationException($"No se encontró persona con el id: {id}");
            return person;
        }
    }
}
