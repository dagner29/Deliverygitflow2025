using Delivery.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using FluentAssertions;
using TheoryAttribute = Xunit.TheoryAttribute;
using Assert = Xunit.Assert;



namespace Delivery.Test.Domain.Delivery
{
    public class AddressTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperly_WithValidParameters()
        {
            // Arrange
            var street = "Main Street";
            var city = "New York";
            var postalCode = "10001";

            // Act
            var address = new Address(street, city, postalCode);

            // Assert
            Assert.NotNull(address);
            Assert.Equal(street, address.Street);
            Assert.Equal(city, address.City);
            Assert.Equal(postalCode, address.PostalCode);
        }

        [Theory]
        [InlineData(null, "New York", "10001")]
        [InlineData("Main Street", null, "10001")]
        [InlineData("Main Street", "New York", null)]
        [InlineData("", "New York", "10001")]
        [InlineData("Main Street", "", "10001")]
        [InlineData("Main Street", "New York", "")]
        public void Constructor_ShouldThrowException_WhenParametersAreInvalid(string street, string city, string postalCode)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Address(street, city, postalCode));
            Assert.Contains("es obligatorio", exception.Message);
        }

    }
}
