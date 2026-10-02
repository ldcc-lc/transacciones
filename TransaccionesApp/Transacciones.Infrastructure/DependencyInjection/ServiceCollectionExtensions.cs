using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Transacciones.Application.Interfaces;
using Transacciones.Infrastructure.Repositories;

namespace Transacciones.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<TransaccionesDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IMonedaRepository, MonedaRepository>();

            return services;
        }
    }
}
