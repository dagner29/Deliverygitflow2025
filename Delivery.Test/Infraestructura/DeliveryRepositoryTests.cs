using Delivery.Domain.Entities;
using Delivery.Infraestructure.Persistence.Repositories;
using Delivery.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Infraestructura
{
    public class DeliveryRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public DeliveryRepositoryTests()
        {
            // Configuración de la base de datos en memoria
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
        }

        private ApplicationDbContext CreateDbContext()
        {
            return new ApplicationDbContext(_dbContextOptions);
        }

        [Fact]
        public async Task AddAsync_ShouldAddDelivery()
        {
            using var context = CreateDbContext();
            var repository = new DeliveryRepository(context);
            var delivery = new Deliveryx(DateTime.Now, 123, 456)
            {
                Status = "Pending"
            };

            // Act
            await repository.AddAsync(delivery);

            // Assert
            var result = await context.Deliveries.FindAsync(delivery.Id);
            Assert.NotNull(result);
            Assert.Equal("Pending", result.Status);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteDelivery()
        {
            using var context = CreateDbContext();
            var repository = new DeliveryRepository(context);
            var delivery = new Deliveryx(DateTime.Now, 123, 456)
            {
                Status = "Pending"
            };
            await repository.AddAsync(delivery);

            // Act
            await repository.DeleteAsync(delivery.Id);

            // Assert
            var result = await context.Deliveries.FindAsync(delivery.Id);
            Assert.Null(result);  // El delivery debe ser eliminado
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllDeliveries()
        {
            using var context = CreateDbContext();
            var repository = new DeliveryRepository(context);
            var delivery1 = new Deliveryx(DateTime.Now, 123, 456)
            {
                Status = "Pending"
            };
            var delivery2 = new Deliveryx(DateTime.Now, 124, 457)
            {
                Status = "Completed"
            };
            await repository.AddAsync(delivery1);
            await repository.AddAsync(delivery2);

            // Act
            var deliveries = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, deliveries.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectDelivery()
        {
            using var context = CreateDbContext();
            var repository = new DeliveryRepository(context);
            var delivery = new Deliveryx(DateTime.Now, 123, 456)
            {
                Status = "Pending"
            };
            await repository.AddAsync(delivery);

            // Act
            var result = await repository.GetByIdAsync(delivery.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(delivery.Id, result.Id);
        }

        [Fact]
        public async Task GetDeliveriesByDateAsync_ShouldReturnDeliveriesForSpecificDate()
        {
            using var context = CreateDbContext();
            var repository = new DeliveryRepository(context);
            var delivery1 = new Deliveryx(DateTime.Now, 123, 456)
            {
                FechaEntrega = DateTime.Today,
                Status = "Pending"
            };
            var delivery2 = new Deliveryx(DateTime.Now, 124, 457)
            {
                FechaEntrega = DateTime.Today.AddDays(1),
                Status = "Completed"
            };
            await repository.AddAsync(delivery1);
            await repository.AddAsync(delivery2);

            // Act
            var deliveries = await repository.GetDeliveriesByDateAsync(DateTime.Today);

            // Assert
            Assert.Single(deliveries);
            Assert.Equal(delivery1.Id, deliveries.First().Id);
        }

        [Fact]
        public async Task AssignDeliveryPersonAsync_ShouldAssignPersonToDelivery()
        {
            using var context = CreateDbContext();
            var repository = new DeliveryRepository(context);
            var delivery = new Deliveryx(DateTime.Now, 123, 456)
            {
                Status = "Pending"
            };
            await repository.AddAsync(delivery);

            var deliveryPersonId = Guid.NewGuid();

            // Act
            await repository.AssignDeliveryPersonAsync(delivery.Id, deliveryPersonId);

            // Assert
            var result = await repository.GetByIdAsync(delivery.Id);
            Assert.Equal(deliveryPersonId, result.DeliveryPersonId);
        }

    }
}
