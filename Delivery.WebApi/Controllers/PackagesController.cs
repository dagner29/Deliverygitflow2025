using Microsoft.AspNetCore.Mvc;
using Delivery.Applications.UsesCases.Packages;
using Delivery.WebApi.DPTOs;
using MediatR;
using Delivery.Domain.Entities;
using Delivery.Applications.Handlers.Packages;
using Delivery.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Azure.Core;

namespace Delivery.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly AddPackageToDeliveryHandler _addPackageHandler;
        private readonly ApplicationDbContext _context;
        //public PackagesController(AddPackageToDeliveryHandler addPackageHandler)
        public PackagesController(ApplicationDbContext context)
        {
            //_addPackageHandler = addPackageHandler;
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        [HttpPost("Adddelivery")]
        public async Task<IActionResult> AddPackageToDelivery([FromBody] PackageDto packageDto)
        {
            // Iniciar una transacción
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (packageDto.deliveryId == Guid.Empty)
                    {
                        return BadRequest(new { message = "El DeliveryId proporcionado no es válido." });
                    }

                    // Verificar si el Deliveryx existe
                    var delivery = await _context.Deliveries.FindAsync(packageDto.deliveryId);
                    if (delivery == null)
                    {
                        return BadRequest(new { message = "El Delivery con ID no existe." });
                    }

                    // Crear un nuevo paquete basado en el DTO
                    var package = new Package
                    {
                        Id = Guid.NewGuid(),
                        DeliveryId = packageDto.deliveryId,
                        ContentDescription = packageDto.ContentDescription,
                        Weight = packageDto.Weight,
                        CreatedAt = DateTime.UtcNow // Asignar la fecha de creación
                    };
                    // Agregar el paquete a la base de datos
                    _context.Packages.Add(package);
                    // Agregar el paquete al Delivery en memoria (si la relación está configurada correctamente)
                    delivery.Packages.Add(package);
                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();
                    // Confirmar la transacción
                    await transaction.CommitAsync();
                    // Retornar el paquete recién creado
                    return CreatedAtAction(nameof(GetPackageById), new { id = package.Id }, package);
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, deshacer los cambios
                    await transaction.RollbackAsync();
                    //return StatusCode(500, $"Error al agregar el paquete: {ex.Message}");
                    return StatusCode(500, new { message = "Ocurrió un error interno.", error = ex.Message });

                }
            }

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(Guid id)
        {
            if (_context.Packages == null)
                return StatusCode(500, "Database context is not initialized.");

            var package = await _context.Packages.FindAsync(id);

            if (package == null)
                return BadRequest("Package with ID no existe.");

            return Ok(package);

        }



    }
}
