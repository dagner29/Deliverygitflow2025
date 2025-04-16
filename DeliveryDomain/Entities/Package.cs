using Delivery.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities
{
    public class Package
    {
        public Guid Id { get;   set; }
        public string ContentDescription { get;   set; }
        public double Weight { get;   set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Clave foránea que referencia a Deliveryx
        public Guid DeliveryId { get; set; }
        
        // Esta propiedad causa el ciclo, así que la ignoramos en la serialización
        [JsonIgnore]
        public Deliveryx Delivery { get; set; }

        public Package() { }

        public Package(string contentDescription, double weight, Guid deliveryId)
        {
            Id = Guid.NewGuid();
            ContentDescription = contentDescription;
            Weight = weight > 0 ? weight : throw new ArgumentException("El peso debe ser mayor que 0.");
            DeliveryId = deliveryId;
            CreatedAt = DateTime.UtcNow;
            //Status = PackageStatus.Pending;
        }

      

    }
}


/*
 
 public enum PackageStatus
{
    Pending,   
    InTransit, 
    Delivered  
}


 */