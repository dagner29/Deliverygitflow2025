using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Delivery.Domain.Entities;
using Delivery.Applications.Interfaces;
using Delivery.Infraestructure.Persistence.Repositories;

namespace Delivery.Infraestructure.Configurations
{
    public static class DatabaseConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepository<Deliveryx>, DeliveryRepository>();
            services.AddScoped<IRepository<Package>, PackageRepository>();
            services.AddScoped<IRepository<DeliveryPerson>, DeliveryPersonRepository>();
        }

        //public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        //    return services;
        //}


        //public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var connectionString = configuration.GetConnectionString("DefaultConnection");
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(connectionString));

        // }
    }
}
