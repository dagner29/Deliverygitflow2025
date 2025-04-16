using Delivery.Applications.Handlers.Deliveries;
using Delivery.Applications.Interfaces;
using Delivery.Applications.UsesCases.Deliveries;
using Delivery.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Aplication.Delivery.EventHanders
{
    public class CreateDeliveryHandlerTests
    {

        [Fact]
        public void Constructor_RepositoryNulo_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CreateDeliveryHandler>>();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new CreateDeliveryHandler(null, mockLogger.Object));
            Assert.Equal("repository", exception.ParamName);
        }

        [Fact]
        public void Constructor_LoggerNulo_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new CreateDeliveryHandler(mockRepository.Object, null));
            Assert.Equal("logger", exception.ParamName);
        }



        [Fact]
        public async Task HandleAsync_ValidCommand_ShouldCreateAndSaveDelivery()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var mockLogger = new Mock<ILogger<CreateDeliveryHandler>>();
            var scheduledDate = DateTime.Now;
            var deliveryAddressId = 123; // Usando int para DeliveryAddressid
            var deliveryRouteId = 456;  // Usando int para DeliveryRouteId

            var handler = new CreateDeliveryHandler(mockRepository.Object, mockLogger.Object);

            var command = new CreateDeliveryCommand
            {
                ScheduledDate = scheduledDate,
                DeliveryAddressid = deliveryAddressId,
                DeliveryRouteId = deliveryRouteId
            };

            // Act
            await handler.HandleAsync(command);

            // Assert
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Deliveryx>()), Times.Once);
        }


        [Fact]
        public async Task HandleAsync_ShouldLogInformationMessages()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var mockLogger = new Mock<ILogger<CreateDeliveryHandler>>();
            var scheduledDate = DateTime.Now;
            var deliveryAddressId = 123; // Cambiado a int según la definición
            var deliveryRouteId = 456;  // Cambiado a int según la definición

            var handler = new CreateDeliveryHandler(mockRepository.Object, mockLogger.Object);

            var command = new CreateDeliveryCommand
            {
                ScheduledDate = scheduledDate,
                DeliveryAddressid = deliveryAddressId,
                DeliveryRouteId = deliveryRouteId
            };

            // Act
            await handler.HandleAsync(command);

            // Assert
            mockLogger.Verify(logger => logger.LogInformation("Iniciando HandleAsync en CreateDeliveryHandler"), Times.Once);
            mockLogger.Verify(logger => logger.LogInformation("Creando delivery con fecha: {ScheduledDate}, dirección: {DeliveryAddress}",
                scheduledDate, deliveryAddressId), Times.Once);
            mockLogger.Verify(logger => logger.LogInformation("Delivery guardado con éxito: {Id}", It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldCallAddAsyncOnce()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Deliveryx>>();
            var mockLogger = new Mock<ILogger<CreateDeliveryHandler>>();
            var scheduledDate = DateTime.Now;
            var deliveryAddressId = 123;  // Cambiado a int según la nueva definición
            var deliveryRouteId = 456;    // Cambiado a int según la nueva definición

            var handler = new CreateDeliveryHandler(mockRepository.Object, mockLogger.Object);

            var command = new CreateDeliveryCommand
            {
                ScheduledDate = scheduledDate,
                DeliveryAddressid = deliveryAddressId,
                DeliveryRouteId = deliveryRouteId
            };

            // Act
            await handler.HandleAsync(command);

            // Assert
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Deliveryx>()), Times.Once);
        }


    }
}
