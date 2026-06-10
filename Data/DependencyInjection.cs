using Data.Persisten;
using Data.Repositories;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    // Inyeccion de depencias por medio de componenetes
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, string stringConnection)
        {
            // Hacer la conexion en a la DB
            services.AddDbContext<AplicationDbContext>(options =>
            options.UseSqlServer(stringConnection));

            services.AddScoped<IRepository<PersonEntity, Guid>, PersonRepository>();
            services.AddScoped<ICodeRepository<PersonEntity>, PersonRepository>();

            return services;
        }
    }
}
