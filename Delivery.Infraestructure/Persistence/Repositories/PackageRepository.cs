using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Delivery.Applications.UsesCases.Interfaces;
using Delivery.Domain.Entities;
using Delivery.Applications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infraestructure.Persistence.Repositories
{

    public class PackageRepository : IRepository<Package>
    {
        private readonly ApplicationDbContext _context;

        public PackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Package entity)
        {
            await _context.Packages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Packages.FindAsync(id);
            if (entity != null)
            {
                _context.Packages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Package>> GetAllAsync()
        {
            return await _context.Packages.ToListAsync();
        }

        public async Task<Package> GetByIdAsync(Guid id)
        {
            return await _context.Packages.FindAsync(id);
        }

        public async Task UpdateAsync(Package entity)
        {
            _context.Packages.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
