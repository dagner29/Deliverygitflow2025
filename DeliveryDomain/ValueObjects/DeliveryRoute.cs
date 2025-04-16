using Delivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.ValueObjects
{
    public class DeliveryRoute
    {

        public int Id { get; set; }  // Clave primaria
        public string StartPoint { get;   set; }
        public string EndPoint { get;   set; }
        public TimeSpan EstimatedTime { get;  set; }

        // Lista de Deliveries que están asociadas a este DeliveryRoute
        // public List<Deliveryx> Deliveries { get; set; }
        public List<Deliveryx> Deliveries { get; set; } = new List<Deliveryx>();

    }
}
