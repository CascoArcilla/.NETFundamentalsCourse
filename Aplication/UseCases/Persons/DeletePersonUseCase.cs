using Domain;
using Domain.Abstractions;

namespace Aplication.UseCases.Persons
{
    public class DeletePersonUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;

        public DeletePersonUseCase(IRepository<PersonEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null) throw new InvalidOperationException("La persona que se intenta borrar no existe.");
            await _repository.DeleteAsync(person);
            await _repository.SaveChangesAsync();
        }
    }
}