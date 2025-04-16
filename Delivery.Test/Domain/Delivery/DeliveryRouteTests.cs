using Delivery.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Domain.Delivery
{
    public class DeliveryRouteTests
    {

        [Fact]
        public void Constructor_SinParametros_DeberiaInicializarCorrectamente()
        {
            // Act
            var deliveryRoute = new DeliveryRoute();

            // Assert
            Assert.NotNull(deliveryRoute);
            Assert.Equal(0, deliveryRoute.Id); // El Id debe estar en su valor predeterminado (0)
            Assert.Null(deliveryRoute.StartPoint); // El StartPoint debe ser nulo
            Assert.Null(deliveryRoute.EndPoint); // El EndPoint debe ser nulo
            Assert.Equal(TimeSpan.Zero, deliveryRoute.EstimatedTime); // El EstimatedTime debe ser TimeSpan.Zero
            Assert.NotNull(deliveryRoute.Deliveries); // La lista Deliveries no debe ser nula
            Assert.Empty(deliveryRoute.Deliveries); // La lista Deliveries debe estar vacía
        }
    }
}
