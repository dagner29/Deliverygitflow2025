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
 using TheoryAttribute = NUnit.Framework.TheoryAttribute;

namespace Delivery.Test.Domain.Packages
{
    
    public class PackageTest
    {


        [Fact]
        public void DefaultConstructor_ShouldInitializeProperly()
        {
            // Act
            var package = new Package();

            // Assert
            Assert.NotNull(package);
            Assert.Equal(Guid.Empty, package.Id);
            Assert.Null(package.ContentDescription);
            Assert.Equal(0, package.Weight);
            Assert.Equal(Guid.Empty, package.DeliveryId);
            Assert.True(package.CreatedAt <= DateTime.UtcNow);
            Assert.Null(package.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldInitializeWithValidParameters()
        {
            // Arrange
            var contentDescription = "Laptop";
            var weight = 2.5;
            var deliveryId = Guid.NewGuid();

            // Act
            var package = new Package(contentDescription, weight, deliveryId);

            // Assert
            Assert.NotNull(package);
            Assert.Equal(contentDescription, package.ContentDescription);
            Assert.Equal(weight, package.Weight);
            Assert.Equal(deliveryId, package.DeliveryId);
            Assert.NotEqual(Guid.Empty, package.Id);
            Assert.True(package.CreatedAt <= DateTime.UtcNow);
        }
        //[Fact]
        //public void Constructor_Should_Create_Package_With_Valid_Data()
        //{
        //    // Arrange
        //    var description = "Electronics";
        //    var weight = 2.5;
        //    var deliveryId = Guid.NewGuid();

        //    // Act
        //    var package = new Package(description, weight, deliveryId);

        //    // Assert
        //    package.Should().NotBeNull();
        //    package.Id.Should().NotBeEmpty();
        //    package.ContentDescription.Should().Be(description);
        //    package.Weight.Should().Be(weight);
        //    package.DeliveryId.Should().Be(deliveryId);
        //    package.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        //}
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_ShouldThrowException_WhenWeightIsInvalid(double invalidWeight)
        {
            // Arrange
            var contentDescription = "Laptop";
            var deliveryId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Package(contentDescription, invalidWeight, deliveryId));
            Assert.Equal("El peso debe ser mayor que 0.", exception.Message);
        }


        [Fact]
        public void Constructor_ShouldSetDefaultTimestamps()
        {
            // Arrange
            var contentDescription = "Libro";
            var weight = 1.2;
            var deliveryId = Guid.NewGuid();

            // Act
            var package = new Package(contentDescription, weight, deliveryId);

            // Assert
            Assert.True(package.CreatedAt <= DateTime.UtcNow);
            Assert.Null(package.UpdatedAt);
        }

        [Fact]
        public void Package_ShouldAllowUpdatingTimestamp()
        {
            // Arrange
            var package = new Package("Celular", 0.8, Guid.NewGuid());
            var newUpdateTime = DateTime.UtcNow.AddHours(1);

            // Act
            package.UpdatedAt = newUpdateTime;

            // Assert
            Assert.NotNull(package.UpdatedAt);
            Assert.Equal(newUpdateTime, package.UpdatedAt);
        }

        //[Fact]
        //public void Constructor_Should_Throw_Exception_When_Weight_Is_Zero_Or_Negative()
        //{
        //    // Arrange
        //    var description = "Books";
        //    var weight = 0;
        //    var deliveryId = Guid.NewGuid();

        //    // Act
        //    Action act = () => new Package(description, weight, deliveryId);

        //    // Assert
        //    act.Should().Throw<ArgumentException>()
        //        .WithMessage("El peso debe ser mayor que 0.");
        //}

        //[Fact]
        //public void Constructor_Should_Set_CreatedAt_To_Current_Time()
        //{
        //    // Act
        //    var package = new Package("Test", 1.0, Guid.NewGuid());

        //    // Assert
        //    package.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        //}

        //[Theory]
        //[InlineData(0)]
        //[InlineData(-1)]
        //[InlineData(-5)]
        //public void Constructor_Should_Throw_Exception_When_Weight_Is_Zero_Or_Negative(double weight)
        //{
        //    // Arrange
        //    var description = "Books";
        //    var deliveryId = Guid.NewGuid();

        //    // Act
        //    Action act = () => new Package(description, weight, deliveryId);

        //    // Assert
        //    act.Should().Throw<ArgumentException>()
        //        .WithMessage("El peso debe ser mayor que 0.");
        //}



    }

}
