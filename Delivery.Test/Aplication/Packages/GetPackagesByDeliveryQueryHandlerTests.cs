//using Delivery.Applications.Interfaces;
using Delivery.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
//using Delivery.Applications.Handlers.Packages;
using Delivery.Applications.UsesCases.Packages;
using GetPackagesByDeliveryQuery = Delivery.Applications.UsesCases.Packages.GetPackagesByDeliveryQuery;
//using Delivery.Applications.Handlers; // Ajusta según la ubicación real de la clase
using Delivery.Applications.Interfaces;
using Delivery.Applications.Handlers.Packages;


namespace Delivery.Test.Aplication.Packages
{

    //public class GetPackagesByDeliveryQueryHandlerTests
    //{
    //    private readonly Mock<IRepository<Deliveryx>> _mockRepository;
    //    private readonly GetPackagesByDeliveryQueryHandler _handler;
    //      public GetPackagesByDeliveryQueryHandlerTests()
    //    {
    //        _mockRepository = new Mock<IRepository<Deliveryx>>();
    //        _handler = new GetPackagesByDeliveryQueryHandler(_mockRepository.Object);
    //    }

    //    [Fact]
    //    public async Task HandleAsync_ShouldReturnPackages_WhenDeliveryExists()
    //    {
    //        // Arrange
    //        var deliveryId = Guid.NewGuid();
    //        var packages = new List<Package>
    //    {
    //        new Package("Laptop", 1.5, deliveryId),
    //        new Package("Phone", 0.5, deliveryId)
    //    };

    //        var delivery = new Deliveryx
    //        {
    //            Id = deliveryId,
    //            ScheduledDate = DateTime.Now,
    //            DeliveryAddressId = 123,
    //            RouteId = 456,
    //            Status = "Pending",
    //            Packages = packages
    //        };

    //        _mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId))
    //            .ReturnsAsync(delivery);

    //        var query = new GetPackagesByDeliveryQuery { DeliveryId = deliveryId };

    //        // Act
    //        var result = await _handler.HandleAsync(query);

    //        // Assert
    //        Assert.NotNull(result);
    //        Assert.Equal(2, result.Count);
    //        Assert.Equal("Laptop", result[0].ContentDescription);
    //        _mockRepository.Verify(repo => repo.GetByIdAsync(deliveryId), Times.Once);
    //    }

    //    [Fact]
    //    public async Task HandleAsync_ShouldThrowKeyNotFoundException_WhenDeliveryDoesNotExist()
    //    {
    //        // Arrange
    //        var deliveryId = Guid.NewGuid();
    //        _mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId))
    //            .ReturnsAsync((Deliveryx)null);

    //        var query = new GetPackagesByDeliveryQuery { DeliveryId = deliveryId };

    //        // Act & Assert
    //        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.HandleAsync(query));
    //        _mockRepository.Verify(repo => repo.GetByIdAsync(deliveryId), Times.Once);
    //    }

    //    [Fact]
    //    public async Task HandleAsync_ShouldReturnEmptyList_WhenDeliveryHasNoPackages()
    //    {
    //        // Arrange
    //        var deliveryId = Guid.NewGuid();
    //        var delivery = new Deliveryx
    //        {
    //            Id = deliveryId,
    //            ScheduledDate = DateTime.Now,
    //            DeliveryAddressId = 123,
    //            RouteId = 456,
    //            Status = "Pending",
    //            Packages = new List<Package>() // No tiene paquetes
    //        };

    //        _mockRepository.Setup(repo => repo.GetByIdAsync(deliveryId))
    //            .ReturnsAsync(delivery);

    //        var query = new GetPackagesByDeliveryQuery { DeliveryId = deliveryId };

    //        // Act
    //        var result = await _handler.HandleAsync(query);

    //        // Assert
    //        Assert.NotNull(result);
    //        Assert.Empty(result);
    //        _mockRepository.Verify(repo => repo.GetByIdAsync(deliveryId), Times.Once);
    //    }

    //    [Fact]
    //    public async Task HandleAsync_ShouldThrowArgumentNullException_WhenQueryIsNull()
    //    {
    //        // Act & Assert
    //        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.HandleAsync(null));
    //    }


    //}

}
