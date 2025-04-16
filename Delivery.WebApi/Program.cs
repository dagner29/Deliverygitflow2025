
using Delivery.Applications.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using Delivery.Infraestructure.Persistence.Repositories;
using Delivery.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Delivery.Applications.UsesCases.Deliveries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MediatR;
using Delivery.Applications.Handlers.Deliveries;
using Delivery.Domain.Entities;
using Delivery.Applications.Handlers.Packages;
//using Delivery.Applications.Interfaces;


var builder = WebApplication.CreateBuilder(args);
// ?? Agregar la configuración de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar repositorios y servicios
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
// Registro de CreateDeliveryHandler

builder.Services.AddScoped<IRepository<Deliveryx>, DeliveryRepository>();
builder.Services.AddScoped<CreateDeliveryHandler>();
builder.Services.AddScoped<AddPackageToDeliveryHandler>();
//builder.Services.AddScoped<IRepository<Deliveryx>, DeliveryxRepository>();
//builder.Services.AddScoped<AddPackageToDeliveryHandler>();

// Inyección de Dependencias
//builder.Services.AddScoped<IRepository<Deliveryx>, DeliveryRepository>();
//builder.Services.AddScoped<CreateDeliveryHandler>();


// Configuración de controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Delivery API", Version = "v1" });
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuración del pipeline de middleware
app.UseRouting();
app.UseAuthorization();
// Mapear los controladores de API
app.MapControllers();
app.Run();