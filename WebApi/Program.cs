using Aplication.UseCases.Persons;
using Data;
using Data.Repositories;
using Domain;
using Domain.Abstractions;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Obtener la cadena de configuracoin a la DB especificada en el appsettings.json,
// si no se encuentra se lanza una excepcion indicando que no hay configuracion a la DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Sin configuracion a la DB");

// Los servicios de la capa de aplicación y de infraestructura se registran aquí
// para que puedan ser inyectados en los controladores o en otros servicios.

// Dependency Injection, these registrations are for the repositories and use cases
builder.Services.AddData(connectionString); // Una forma de inyectar las depencias abajo por medio de componenete
//builder.Services.AddScoped<IRepository<PersonEntity, Guid>, PersonRepository>();
//builder.Services.AddScoped<ICodeRepository<PersonEntity>, PersonRepository>();

// Register Use Cases
builder.Services.AddScoped<CreatePersonUseCase>();
builder.Services.AddScoped<GetPersonByIdUseCase>();
builder.Services.AddScoped<GetPersonByCodeUseCase>();
builder.Services.AddScoped<GetAllPersonsUseCase>();
builder.Services.AddScoped<UpdatePersonUseCase>();
builder.Services.AddScoped<DeletePersonUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPersonsEndpoints();

app.Run();

/*
// Esto es un ejemplo de metodos de extension
// Una forma de agregar nuevos metodos a una clase ya compilada

string name = "Bobi";

// Este metodo no existe dentro de la clase string, pero lo podemos agregar con un metodo de extension

Console.WriteLine(name.Run());

// Este es el ejemplo de agregar un metodo de extension a la clase string,
// el metodo se llama Run y devuelve un mensaje indicando que el string esta corriendo

public static class StringExtensions
{
    // Se indica que el primer parametro es del tipo string y se le asigna el nombre str,
    // esto indica que el metodo de extension se aplicara a la clase string

    public static string Run(this string str)
    {
        return $"El string {str} esta corriendo";
    }
}
*/