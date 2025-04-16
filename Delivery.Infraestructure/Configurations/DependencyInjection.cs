using Delivery.Domain.Abstractions;
using Delivery.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infraestructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<IRepository<Delivery>, DeliveryRepository>();

            // Servicios adicionales
            // Si tienes otros servicios que pertenecen a esta capa, agrégalos aquí.

            // Controlador de eventos de dominio
            services.AddScoped<DomainEventDispatcher>();

            return services;
        }
    }
}
