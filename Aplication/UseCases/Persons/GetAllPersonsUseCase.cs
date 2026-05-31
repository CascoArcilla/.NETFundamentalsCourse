using Domain;
using Domain.Abstractions;

namespace Aplication.UseCases.Persons
{
    // Al agregar el domino en depencias de este proyecto podemos usar las entidades y repositorios definidos

    public class GetAllPersonsUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;

        public GetAllPersonsUseCase(IRepository<PersonEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonEntity>> ExecuteAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
