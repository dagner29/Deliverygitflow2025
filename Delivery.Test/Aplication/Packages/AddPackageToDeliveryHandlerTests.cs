using Delivery.Applications.Handlers.Packages;
using Delivery.Applications.Interfaces;
using Delivery.Applications.UsesCases.Packages;
using Delivery.Domain.Entities;

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Aplication.Packages
{
    public class AddPackageToDeliveryHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ValidCommand_ShouldAddPackageAndSave()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var deliveryId = Guid.NewGuid();
            var package = new Package("Electronics", 2.5, deliveryId); // Ajustado con ContentDescription y Weight

            var delivery = new Deliveryx
            {
                Id = deliveryId,
                ScheduledDate = DateTime.Now,
                DeliveryAddressId = 123,
                RouteId = 456,
                Status = "Pending"
            };

            var command = new AddPackageToDeliveryCommand
            {
                DeliveryId = deliveryId,
                Package = package
            };

            mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync(delivery);
            mockRepository.Setup(repo => repo.UpdateAsync(delivery)).Returns(Task.CompletedTask);

            var handler = new AddPackageToDeliveryHandler(mockRepository.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            Assert.Contains(package, delivery.Packages);
            Assert.Equal(deliveryId, package.DeliveryId);
            mockRepository.Verify(repo => repo.UpdateAsync(delivery), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_DeliveryNotFound_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var deliveryId = Guid.NewGuid();
            var command = new AddPackageToDeliveryCommand
            {
                DeliveryId = deliveryId,
                Package = new Package("Books", 1.2, deliveryId)
            };

            mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync((Deliveryx)null);

            var handler = new AddPackageToDeliveryHandler(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.HandleAsync(command));
            Assert.Equal("Delivery not found.", exception.Message);
        }

        [Fact]
        public async Task HandleAsync_NullPackage_ShouldThrowArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var deliveryId = Guid.NewGuid();
            var delivery = new Deliveryx
            {
                Id = deliveryId,
                ScheduledDate = DateTime.Now,
                DeliveryAddressId = 123,
                RouteId = 456,
                Status = "Pending"
            };

            var command = new AddPackageToDeliveryCommand { DeliveryId = deliveryId, Package = null };

            mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync(delivery);

            var handler = new AddPackageToDeliveryHandler(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync(command));
            Assert.Equal("Package cannot be null.", exception.Message);
        }

        [Fact]
        public async Task HandleAsync_InvalidWeight_ShouldThrowArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var deliveryId = Guid.NewGuid();
            var invalidPackage = new Package("Fragile Items", -2.0, deliveryId); // Peso inválido

            var delivery = new Deliveryx
            {
                Id = deliveryId,
                ScheduledDate = DateTime.Now,
                DeliveryAddressId = 123,
                RouteId = 456,
                Status = "Pending"
            };

            var command = new AddPackageToDeliveryCommand { DeliveryId = deliveryId, Package = invalidPackage };

            mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync(delivery);

            var handler = new AddPackageToDeliveryHandler(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync(command));
            Assert.Equal("El peso debe ser mayor que 0.", exception.Message);
        }
    }

    //public class AddPackageToDeliveryHandlerTests
    //{
    //    [Fact]
    //    public async Task HandleAsync_ValidCommand_ShouldAddPackageAndSave()
    //    {
    //        // Arrange
    //        var mockRepository = new Mock<IRepository<Deliveryx>>();
    //        var deliveryId = Guid.NewGuid();
    //        var package = new Package { Id = Guid.NewGuid(), Description = "Paquete 1" };

    //        var delivery = new Deliveryx
    //        {
    //            Id = deliveryId,
    //            ScheduledDate = DateTime.Now,
    //            DeliveryAddressId = 123,
    //            RouteId = 456,
    //            Status = "Pending"
    //        };

    //        var command = new AddPackageToDeliveryCommand
    //        {
    //            DeliveryId = deliveryId,
    //            Package = package
    //        };

    //        mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync(delivery);
    //        mockRepository.Setup(repo => repo.UpdateAsync(delivery)).Returns(Task.CompletedTask);

    //        var handler = new AddPackageToDeliveryHandler(mockRepository.Object);

    //        // Act
    //        await handler.HandleAsync(command);

    //        // Assert
    //        Assert.Contains(package, delivery.Packages);
    //        mockRepository.Verify(repo => repo.UpdateAsync(delivery), Times.Once);
    //    }

    //    [Fact]
    //    public async Task HandleAsync_DeliveryNotFound_ShouldThrowKeyNotFoundException()
    //    {
    //        // Arrange
    //        var mockRepository = new Mock<IRepository<Deliveryx>>();
    //        var deliveryId = Guid.NewGuid();
    //        var command = new AddPackageToDeliveryCommand { DeliveryId = deliveryId, Package = new Package() };

    //        mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync((Deliveryx)null);

    //        var handler = new AddPackageToDeliveryHandler(mockRepository.Object);

    //        // Act & Assert
    //        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.HandleAsync(command));
    //        Assert.Equal("Delivery not found.", exception.Message);
    //    }

    //    [Fact]
    //    public async Task HandleAsync_NullPackage_ShouldThrowArgumentException()
    //    {
    //        // Arrange
    //        var mockRepository = new Mock<IRepository<Deliveryx>>();
    //        var deliveryId = Guid.NewGuid();
    //        var delivery = new Deliveryx
    //        {
    //            Id = deliveryId,
    //            ScheduledDate = DateTime.Now,
    //            DeliveryAddressId = 123,
    //            RouteId = 456,
    //            Status = "Pending"
    //        };

    //        var command = new AddPackageToDeliveryCommand { DeliveryId = deliveryId, Package = null };

    //        mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync(delivery);

    //        var handler = new AddPackageToDeliveryHandler(mockRepository.Object);

    //        // Act & Assert
    //        var exception = await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync(command));
    //        Assert.Equal("Package cannot be null.", exception.Message);
    //    }

    //}
}
