using Delivery.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities
{
    public class Deliveryx
    {
        public Guid Id { get;  set; }
        public DateTime ScheduledDate { get;  set; }
        public int DeliveryAddressId { get; set; }  // Clave foránea
        public int RouteId { get; set; }  // 🔥 CAMBIADO de DeliveryRouteId a RouteId
        public string Status { get; set; }
        public Guid? DeliveryPersonId { get; set; }  // Se permite `NULL` como en la BD
        public DateTime FechaEntrega { get; set; }
        // Relaciones
        public DeliveryRoute Route { get;  set; }
        public DeliveryPerson AssignedPerson { get;  set; }
        public List<Package> Packages { get;  set; }

         
        public Deliveryx() { }

        public Deliveryx(DateTime scheduledDate, int deliveryAddressId, int routeId)
        {   
            Id = Guid.NewGuid();
            ScheduledDate = scheduledDate;
            DeliveryAddressId = deliveryAddressId;
            RouteId = routeId; // 🔥 CAMBIADO de DeliveryRouteId a RouteId
            Packages = new List<Package>();
            Status = "Pending";
        }


        public void AssignDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            AssignedPerson = deliveryPerson;
        }

        public void AddPackage(Package package)
        {

            if (Packages == null)
                Packages = new List<Package>();
            Packages.Add(package);
            //Packages.Add(package);
        }

    }
}
