using Delivery.Applications.Handlers.Deliveries;
using Delivery.Applications.UsesCases.Deliveries;
//using Delivery.Applications.UsesCases.Interfaces;
using Delivery.Applications.Interfaces;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Xunit;

namespace Delivery.Test.Aplication.Delivery.EventHanders
{
    public class AssignDeliveryToPersonHandlerTests
    {
        [Fact]
        public async Task HandleAsync_DeliveryAndPersonExist_ShouldAssignDeliveryPersonAndUpdate()
        {
            // Arrange
            var mockDeliveryRepository = new Mock<IRepository<Deliveryx>>();
            var mockPersonRepository = new Mock<IRepository<DeliveryPerson>>();

            var deliveryId = Guid.NewGuid();
            var personId = Guid.NewGuid();

            var delivery = new Deliveryx { Id = deliveryId };
            var person = new DeliveryPerson { Id = personId, Name = "Juan" };

            mockDeliveryRepository.Setup(repo => repo.GetByIdAsync(deliveryId)).ReturnsAsync(delivery);
            mockPersonRepository.Setup(repo => repo.GetByIdAsync(personId)).ReturnsAsync(person);
            mockDeliveryRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Deliveryx>())).Returns(Task.CompletedTask);

            var handler = new AssignDeliveryToPersonHandler(mockDeliveryRepository.Object, mockPersonRepository.Object);

            var command = new AssignDeliveryToPersonCommand { DeliveryId = deliveryId, DeliveryPersonId = personId };

            // Act
            await handler.HandleAsync(command);

            // Assert
            mockDeliveryRepository.Verify(repo => repo.GetByIdAsync(deliveryId), Times.Once);
            mockPersonRepository.Verify(repo => repo.GetByIdAsync(personId), Times.Once);
            mockDeliveryRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Deliveryx>()), Times.Once);
        }
    }

}
