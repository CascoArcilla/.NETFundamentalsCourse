using Aplication.DTOs.Persons;
using Aplication.UseCases.Persons;
using Azure.Core;

namespace WebApi.Endpoints
{
    public static class PersonEndpoint
    {
        public static void MapPersonsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/persons").WithTags("Persons");

            group.MapGet("/{id:guid}", async (Guid id, GetPersonByIdUseCase useCase) =>
            {
                try
                {
                    var persons = await useCase.ExecuteAsync(id);
                    return Results.Ok(persons);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
            })
            .WithName("GetPersonsById")
            .WithSummary("Obtener una persona por su ID")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (CreatePersonDto dto, CreatePersonUseCase useCase) =>
            {
                try
                {
                    var person = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/persons/{person.Id}", person);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(new { error = ex.Message });
                }
            }).WithName("CreatePerson")
            .WithSummary("Crear una persona")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
