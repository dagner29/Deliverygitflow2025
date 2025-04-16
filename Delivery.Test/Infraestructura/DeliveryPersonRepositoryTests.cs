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
using Microsoft.EntityFrameworkCore.InMemory;
//using Microsoft.EntityFrameworkCore.InMemory;

namespace Delivery.Test.Infraestructura
{
    public class DeliveryPersonRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public DeliveryPersonRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldAddDeliveryPerson()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new DeliveryPersonRepository(context);
            var deliveryPerson = new DeliveryPerson { Id = Guid.NewGuid(), Name = "John Doe" };

            // Act
            await repository.AddAsync(deliveryPerson);

            // Assert
            var result = await context.DeliveryPersons.FindAsync(deliveryPerson.Id);
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDeliveryPerson_WhenExists()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new DeliveryPersonRepository(context);
            var deliveryPerson = new DeliveryPerson { Id = Guid.NewGuid(), Name = "Alice" };
            context.DeliveryPersons.Add(deliveryPerson);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdAsync(deliveryPerson.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alice", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new DeliveryPersonRepository(context);

            // Act
            var result = await repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllDeliveryPersons()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new DeliveryPersonRepository(context);
            context.DeliveryPersons.AddRange(
                new DeliveryPerson { Id = Guid.NewGuid(), Name = "Person 1" },
                new DeliveryPerson { Id = Guid.NewGuid(), Name = "Person 2" }
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveDeliveryPerson_WhenExists()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new DeliveryPersonRepository(context);
            var deliveryPerson = new DeliveryPerson { Id = Guid.NewGuid(), Name = "Bob" };
            context.DeliveryPersons.Add(deliveryPerson);
            await context.SaveChangesAsync();

            // Act
            await repository.DeleteAsync(deliveryPerson.Id);
            var result = await context.DeliveryPersons.FindAsync(deliveryPerson.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingDeliveryPerson()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new DeliveryPersonRepository(context);
            var deliveryPerson = new DeliveryPerson { Id = Guid.NewGuid(), Name = "Old Name" };
            context.DeliveryPersons.Add(deliveryPerson);
            await context.SaveChangesAsync();

            // Act
            deliveryPerson.Name = "New Name";
            await repository.UpdateAsync(deliveryPerson);
            var result = await context.DeliveryPersons.FindAsync(deliveryPerson.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Name", result.Name);
        }

    }
}
