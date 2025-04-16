using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Delivery.Domain.Entities;



using Delivery.Applications.Interfaces;
 

namespace Delivery.Infraestructure.Persistence.Repositories
{
    public class DeliveryPersonRepository : IRepository<DeliveryPerson>
    {
        private readonly ApplicationDbContext _context;

        public DeliveryPersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DeliveryPerson entity)
        {
            await _context.DeliveryPersons.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.DeliveryPersons.FindAsync(id);
            if (entity != null)
            {
                _context.DeliveryPersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DeliveryPerson>> GetAllAsync()
        {
            return await _context.DeliveryPersons.ToListAsync();
        }

        public async Task<DeliveryPerson> GetByIdAsync(Guid id)
        {
            return await _context.DeliveryPersons.FindAsync(id);
        }

        public async Task UpdateAsync(DeliveryPerson entity)
        {
            _context.DeliveryPersons.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
