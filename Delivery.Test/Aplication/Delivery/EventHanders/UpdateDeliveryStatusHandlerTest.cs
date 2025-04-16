//using Delivery.Applications.UsesCases.Interfaces;
using Moq;
using Delivery.Applications.Handlers.Deliveries;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Applications.UsesCases.Deliveries;
using Delivery.Domain.Entities;
using MediatR;
using Delivery.Applications.Interfaces;
using Xunit;

namespace Delivery.Test.Aplication.Delivery.EventHanders
{
    public class UpdateDeliveryStatusHandlerTests
    {
        [Fact]
        public async Task HandleAsync_DeliveryFound_ShouldUpdateDelivery()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var deliveryId = Guid.NewGuid();
            var command = new UpdateDeliveryStatusCommand { DeliveryId = deliveryId };

            var delivery = new Deliveryx
            {
                Id = deliveryId,
                ScheduledDate = DateTime.Now,
                DeliveryAddressId = 123,    // Ajustado a DeliveryAddressId
                RouteId = 456,              // Ajustado a RouteId
                Status = "Pending",         // Status inicial
                FechaEntrega = DateTime.MinValue
            };


            mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync(delivery);
            mockRepository.Setup(repo => repo.UpdateAsync(delivery)).Returns(Task.CompletedTask);

            var handler = new UpdateDeliveryStatusHandler(mockRepository.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            mockRepository.Verify(repo => repo.GetByIdAsync(deliveryId), Times.Once);
            mockRepository.Verify(repo => repo.UpdateAsync(delivery), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_DeliveryNotFound_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var deliveryId = Guid.NewGuid();
            var command = new UpdateDeliveryStatusCommand { DeliveryId = deliveryId };

            mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync((Deliveryx)null);

            var handler = new UpdateDeliveryStatusHandler(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.HandleAsync(command));
            Assert.Equal("Delivery not found.", exception.Message);
        }
    }
}
