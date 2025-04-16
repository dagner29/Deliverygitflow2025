using Delivery.Domain.Entities;
using Moq;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Delivery.Domain.ValueObjects;
using FluentAssertions;
using TheoryAttribute = Xunit.TheoryAttribute;
using Assert = Xunit.Assert;
  


namespace Delivery.Test.Domain.Delivery
{
    public  class DeliveryPersonTests
    {
        [Fact]
        public void Constructor_SinParametros_DeberiaInicializarPropiedades()
        {
            // Act
            var deliveryPerson = new DeliveryPerson();

            // Assert
            Assert.NotNull(deliveryPerson);
            Assert.Empty(deliveryPerson.Deliveries); // La lista de entregas debe estar vacía por defecto
            Assert.Equal(Guid.Empty, deliveryPerson.Id); // El GUID debe estar vacío inicialmente
            Assert.Null(deliveryPerson.Name); // El nombre debe ser nulo
        }



        [Fact]
        public void Constructor_ConName_DeberiaInicializarCorrectamente()
        {
            // Act
            var deliveryPerson = new DeliveryPerson("Juan");

            // Assert
            Assert.NotNull(deliveryPerson);
            Assert.Equal("Juan", deliveryPerson.Name); // Verifica que el nombre sea el esperado
            Assert.NotEqual(Guid.Empty, deliveryPerson.Id); // El GUID debe ser un valor válido
        }

        [Fact]
        public void Constructor_ConNameVacio_OExcepcionArgumento()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new DeliveryPerson(""));
            Assert.Equal("El nombre del repartidor no puede estar vacío. (Parameter 'name')", exception.Message);
        }


        [Fact]
        public void Constructor_ConNameNulo_OExcepcionArgumento()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new DeliveryPerson(null));
            Assert.Equal("El nombre del repartidor no puede estar vacío. (Parameter 'name')", exception.Message);
        }

        [Fact]
        public void Constructor_SinParametros_ListaDeDeliveriesVacía()
        {
            // Act
            var deliveryPerson = new DeliveryPerson();

            // Assert
            Assert.NotNull(deliveryPerson.Deliveries); // La lista no debe ser nula
            Assert.Empty(deliveryPerson.Deliveries); // La lista debe estar vacía
        }

        //    [Fact]
        //    public void DefaultConstructor_ShouldInitializeProperly()
        //    {
        //        // Act
        //        var package = new Package();

        //        // Assert
        //        Assert.NotNull(package);
        //        Assert.Equal(Guid.Empty, package.Id);   // ✅ Cambiado AreEqual -> Equal
        //        Assert.Null(package.ContentDescription);
        //        Assert.Equal(0, package.Weight);        // ✅ Cambiado AreEqual -> Equal
        //        Assert.Equal(Guid.Empty, package.DeliveryId); // ✅ Cambiado AreEqual -> Equal
        //        Assert.True(package.CreatedAt <= DateTime.UtcNow);
        //        Assert.Null(package.UpdatedAt);
        //    }

        //    [Theory]
        //    [InlineData("")]
        //    [InlineData(" ")]
        //    [InlineData(null)]
        //    public void Constructor_ShouldThrowException_WhenNameIsInvalid(string invalidName)
        //    {
        //        // Act & Assert
        //        var exception = Assert.Throws<ArgumentException>(() => new DeliveryPerson(invalidName));
        //        Assert.Equal("El nombre del repartidor no puede estar vacío. (Parameter 'name')", exception.Message);
        //    }

        //    [Fact]
        //    public void AssignDelivery_ShouldAddDeliveryToDeliveriesList()
        //    {
        //        // Arrange
        //        var deliveryPerson = new DeliveryPerson("Juan Pérez");
        //        var mockDelivery = new Mock<Deliveryx>();  // Simulación con Moq

        //        // Act
        //        //deliveryPerson.AssignDelivery(mockDelivery.Object);
        //        deliveryPerson.Deliveries.Add(mockDelivery.Object);  // Se asume que la lista es pública y accesible

        //        // Assert

        //        //Assert.Single(deliveryPerson.Deliveries); // Debería haber solo una entrega en la lista

        //        //Assert.Contains(mockDelivery.Object, deliveryPerson.Deliveries);
        //    }
        //    //[Fact]
        //    //public void DeliveryPerson_CanBeInstantiated_WithDependencies()
        //    //{
        //    //    // Arrange
        //    //    var mockService = new Mock<ISomeDependency>();

        //    //    // Act
        //    //    var deliveryPerson = new DeliveryPerson(mockService.Object);

        //    //    // Assert
        //    //    Assert.NotNull(deliveryPerson);
        //    //}
    }
}
