using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.ValueObjects;
using Delivery.Domain.Entities;
using FluentAssertions;
using Xunit;
using Moq;

namespace Delivery.Test.Domain.Delivery
{
    public class DeliveryTest
    {
        [Fact]
        public void Constructor_Should_Initialize_Correctly()
        {
            // Arrange
            DateTime scheduledDate = DateTime.UtcNow;
            int addressId = 1001;
            int routeId = 2001;

            // Act
            var delivery = new Deliveryx(scheduledDate, addressId, routeId);

            // Assert
            delivery.Should().NotBeNull();
            delivery.ScheduledDate.Should().Be(scheduledDate);
            delivery.DeliveryAddressId.Should().Be(addressId);
            delivery.RouteId.Should().Be(routeId);
            delivery.Status.Should().Be("Pending");
            delivery.Packages.Should().BeEmpty();
        }

        [Fact]
        public void AddPackage_Should_Add_Package_To_List()
        {
            // Arrange
            var delivery = new Deliveryx(DateTime.UtcNow, 1001, 2001);
            var package = new Package("Electronics", 2.5, delivery.Id);

            // Act
            delivery.AddPackage(package);

            // Assert
            delivery.Packages.Should().ContainSingle();
            delivery.Packages[0].Should().Be(package);
        }

        //[Fact]
        //public void AssignDeliveryPerson_Should_Set_AssignedPerson()
        //{
        //    // Arrange
        //    var delivery = new Deliveryx(DateTime.UtcNow, 1001, 2001);
        //    var deliveryPerson = new DeliveryPerson { Id = Guid.NewGuid(), Name = "John Doe" };

        //    // Act
        //    delivery.AssignDeliveryPerson(deliveryPerson);

        //    // Assert
        //    delivery.AssignedPerson.Should().Be(deliveryPerson);
        //}

        [Fact]
        public void AssignDeliveryPerson_Should_Set_AssignedPerson()
        {
            // Arrange
            var delivery = new Deliveryx(DateTime.UtcNow, 1001, 2001);
            var mockPerson = new Mock<DeliveryPerson>();
            mockPerson.Setup(p => p.Id).Returns(Guid.NewGuid());
            mockPerson.Setup(p => p.Name).Returns("John Doe");

            // Act
            delivery.AssignDeliveryPerson(mockPerson.Object);

            // Assert
            delivery.AssignedPerson.Should().NotBeNull();
            delivery.AssignedPerson.Name.Should().Be("John Doe");
        }

        [Fact]
        public void SetRoute_Should_Assign_Route()
        {
            // Arrange
            var delivery = new Deliveryx(DateTime.UtcNow, 1001, 2001);
            var mockRoute = new Mock<DeliveryRoute>();
            mockRoute.Setup(r => r.Id).Returns(3001);
            mockRoute.Setup(r => r.StartPoint).Returns("Warehouse A");
            mockRoute.Setup(r => r.EndPoint).Returns("Customer B");

            // Act
            //delivery.Route = mockRoute.Object;
            delivery.Route = mockRoute.Object;

            // Assert
            delivery.Route.Should().NotBeNull();
            delivery.Route.StartPoint.Should().Be("Warehouse A");
            delivery.Route.EndPoint.Should().Be("Customer B");
        }
    }
}
