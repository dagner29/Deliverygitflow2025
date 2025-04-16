using Delivery.WebApi.DPTOs;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Delivery.Domain.ValueObjects;
using Delivery.Applications.UsesCases.Deliveries;
using Delivery.Applications.Handlers.Deliveries;
using System.Net;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Delivery.Infraestructure.Persistence;
using Delivery.Domain.Entities;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;



namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly CreateDeliveryHandler _createHandler;
        private readonly ApplicationDbContext _context;
        public DeliveriesController(CreateDeliveryHandler createHandler, ApplicationDbContext context)
        {
            _createHandler = createHandler;
            _context = context ?? throw new ArgumentNullException(nameof(context)); // 🚨 Se asegura de que no sea nulo

        }

        [HttpPost("Add-Delivery")]
        public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryRequestDto requestDto)
        {
            if (requestDto == null)
                return BadRequest("El cuerpo de la solicitud es nulo.");
            //  Validación de fechas para evitar errores de SQL Server
            if (requestDto.ScheduledDate < new DateTime(1753, 1, 1))
                requestDto.ScheduledDate = DateTime.UtcNow;

            if (requestDto.FechaEntrega < new DateTime(1753, 1, 1))
                requestDto.FechaEntrega = DateTime.UtcNow;
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                //  Crear y guardar la dirección antes de crear el delivery
                var address = new Address(requestDto.Street, requestDto.City, requestDto.PostalCode);
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();  // Guardamos para obtener el ID

                //  Verificar si existe la ruta, si no, crearla
                var route = await _context.DeliveryRoute.FindAsync(requestDto.DeliveryRouteId);
                if (route == null)
                {
                    route = new DeliveryRoute
                    {
                        StartPoint = "Default StartPoint",  // 🔴 Asegurar valores no nulos
                        EndPoint = "Default EndPoint"
                    };
                    _context.DeliveryRoute.Add(route);
                    await _context.SaveChangesAsync();
                }
                var command = new CreateDeliveryCommand
                {
                    ScheduledDate = requestDto.ScheduledDate,
                    DeliveryAddressid = address.Id, // Usamos el ID de la dirección guardada
                    DeliveryRouteId = route.Id  // 🚀 Ahora estamos seguros de que la ruta existe
                };

                await _createHandler.HandleAsync(command);
                // Confirmar la transacción
                await transaction.CommitAsync();

                return Ok("Entrega creada exitosamente.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error al crear la entrega: {ex.Message}");
            }
        }

        [HttpPut("Assign-person")]
        public async Task<IActionResult> AssignDeliveryPerson([FromBody] AssignDeliveryPersonDto requestDto)
        {
            if (requestDto == null || requestDto.DeliveryPersonId == Guid.Empty || requestDto.deliveryId == Guid.Empty)
            {
                return BadRequest(new { message = "Debe proporcionar un DeliveryPersonId y un DeliveryId válidos." });
            }

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //  Buscar el delivery en la base de datos
                    var delivery = await _context.Deliveries.FindAsync(requestDto.deliveryId);
                    if (delivery == null)
                    {
                        return BadRequest(new { message = "El Delivery especificado no existe." });
                    }

                    //  Buscar el repartidor en la base de datos
                    var deliveryPerson = await _context.DeliveryPersons.FindAsync(requestDto.DeliveryPersonId);
                    if (deliveryPerson == null)
                    {
                        return BadRequest(new { message = "El DeliveryPerson especificado no existe." });
                    }

                    //  Verificar si el Delivery ya tiene asignado un repartidor
                    if (delivery.DeliveryPersonId == requestDto.DeliveryPersonId)
                    {
                        return BadRequest(new { message = "El Delivery ya tiene asignado este repartidor." });
                    }

                    //  Asignar el DeliveryPersonId al Delivery
                    delivery.DeliveryPersonId = requestDto.DeliveryPersonId;

                    //  Guardar los cambios
                    await _context.SaveChangesAsync();

                    // Confirmar la transacción
                    await transaction.CommitAsync();

                    return NoContent(); // 204 - No Content
                }
                catch (Exception ex)
                {
                    // Revertir la transacción en caso de error
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { message = "Error al asignar el repartidor.", error = ex.Message });
                }
            }
           
        }


        [HttpPost("Add-Delivery-Person")]
        public async Task<IActionResult> AddAndAssignDeliveryPerson([FromBody] CreateDeliveryPersonDto requestDto)
        {
            if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.Name))
            {
                return BadRequest(new { message = "Error. El nombre del repartidor es obligatorio." });
            }

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 📌 Validar si el nombre ya existe
                    bool exists = await _context.DeliveryPersons.AnyAsync(dp => dp.Name == requestDto.Name);
                    if (exists)
                    {
                        return Conflict(new { message = "Error. El nombre del repartidor ya esta registrado." });
                    }
                    // 📌 Crear el nuevo DeliveryPerson
                    var deliveryPerson = new DeliveryPerson
                    {
                        Name = requestDto.Name
                    };
                    _context.DeliveryPersons.Add(deliveryPerson);
                    await _context.SaveChangesAsync(); // Guardamos para obtener el ID
                    // 📌 Confirmar la transacción
                    await transaction.CommitAsync();
                    return Ok(new { message = "Repartidor agregado y asignado correctamente", deliveryPersonId = deliveryPerson.Id });
                }
                catch (Exception ex)
                {
                    // 📌 En caso de error, revertir la transacción
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { message = "Error al agregar el repartidor.", error = ex.Message });
                }
            }
        }


        [HttpPut("Update-Status")]
        public async Task<IActionResult> UpdateDeliveryStatus([FromBody] DeliveryidDto requestDto)
        
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Buscar el Delivery por ID
                    var delivery = await _context.Deliveries.FindAsync(requestDto.Id);
                    if (delivery == null)
                    {
                        return BadRequest(new { message = "No se encontró el Delivery." });
                    }
                    // Verificar si el estado ya es "Realizado"
                    if (delivery.Status == "Realizado")
                    {
                        return BadRequest(new { message = "El Delivery ya esta en estado REALIZADO." });
                    }
                    // Actualizar estado a "Realizado"
                    delivery.Status = "Realizado";
                    // Guardar cambios en la base de datos
                    await _context.SaveChangesAsync();
                    // Confirmar la transacción
                    await transaction.CommitAsync();
                    return NoContent(); // Respuesta 204 sin contenido
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, deshacer los cambios
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { message = "Ocurrió un error al actualizar el estado del Delivery.", error = ex.Message });
                }
            }

        }






    }


}














//if (requestDto == null || requestDto.DeliveryPersonId == Guid.Empty)
//    return BadRequest("Debe proporcionar un DeliveryPersonId válido.");

//// 🔍 Buscar el delivery en la base de datos
//var delivery = await _context.Deliveries.FindAsync(requestDto.deliveryId);
//if (delivery == null)
//    return NotFound("El Deliveryx especificado no existe.");

//// 🔍 Buscar el repartidor en la base de datos
//var deliveryPerson = await _context.DeliveryPersons.FindAsync(requestDto.DeliveryPersonId);
//if (deliveryPerson == null)
//    return NotFound("El DeliveryPerson especificado no existe.");

//// ✅ Asignar el DeliveryPersonId al Deliveryx
//delivery.DeliveryPersonId = requestDto.DeliveryPersonId;

//// 💾 Guardar los cambios
//await _context.SaveChangesAsync();

//// Retornar sin serializar todo el objeto
//return NoContent();



/*
// Buscar el Deliveryx por ID
var delivery = await _context.Deliveries.FindAsync(id);

if (delivery == null)
{
    return NotFound("No se encontró el Delivery.");
}
// Verificar si el estado ya es "Realizado"
if (delivery.Status == "Realizado")
{
    return BadRequest("El Delivery ya está en estado 'Realizado'.");
}
// Actualizar estado a "Realizado"
delivery.Status = "Realizado";
// Guardar cambios en la base de datos
await _context.SaveChangesAsync();

return NoContent(); // Respuesta 204 sin contenido
*/








/*
            // Crear y guardar la dirección antes de crear el delivery
            var address = new Address(requestDto.Street, requestDto.City, requestDto.PostalCode);
            // 🚨 Asegurar que _context no sea null
            if (_context == null || _context.Addresses == null)
                return StatusCode(500, "Error interno: El contexto de base de datos no está inicializado.");

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();  // Guardamos para obtener el ID


            // ✅ Verificar si la ruta existe, si no, crearla
            var route = await _context.DeliveryRoute.FindAsync(requestDto.DeliveryRouteId);

            if (route == null)
            {
                route = new DeliveryRoute
                {
                    StartPoint = "Default StartPoint",  // 🔴 Asegurar valores no nulos
                    EndPoint = "Default EndPoint"
                };
                _context.DeliveryRoute.Add(route);
                await _context.SaveChangesAsync();
            }

        var command = new CreateDeliveryCommand 
            {
                ScheduledDate = requestDto.ScheduledDate,
                DeliveryAddressid = address.Id, // Usamos el ID de la dirección guardada
                DeliveryRouteId = route.Id  // 🚀 Ahora estamos seguros de que la ruta existe
            };

            await _createHandler.HandleAsync(command);
            return Ok();

*/