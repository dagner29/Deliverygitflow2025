using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Applications.Interfaces;
using Delivery.Domain.Entities;
//using Delivery.Applications.Interfaces;
using Delivery.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Delivery.Infraestructure.Persistence;

namespace Delivery.Infraestructure.Persistence.Repositories
{

    public class DeliveryRepository : IDeliveryRepository//<Deliveryx>
    {
        private readonly ApplicationDbContext _context;

        public DeliveryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Deliveryx entity)
        {
            await _context.Deliveries.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Deliveries.FindAsync(id);
            if (entity != null)
            {
                _context.Deliveries.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Deliveryx>> GetAllAsync()
        {
            return await _context.Deliveries.ToListAsync();
        }

        public async Task<Deliveryx> GetByIdAsync(Guid id)
        {
            return await _context.Deliveries.FindAsync(id);
        }

        public async Task UpdateAsync(Deliveryx entity)
        {
            _context.Deliveries.Update(entity);
            await _context.SaveChangesAsync();
        }


        // Métodos adicionales de IDeliveryRepository
        public async Task<IEnumerable<Deliveryx>> GetDeliveriesByDateAsync(DateTime date)
        {
            return await _context.Deliveries
                .Where(d => d.FechaEntrega.Date == date.Date)
                .ToListAsync();
        }

        public async Task AssignDeliveryPersonAsync(Guid deliveryId, Guid deliveryPersonId)
        {
            var delivery = await _context.Deliveries.FindAsync(deliveryId);
            if (delivery != null)
            {
                delivery.DeliveryPersonId = deliveryPersonId;
                await _context.SaveChangesAsync();
            }
        }
    }


}
