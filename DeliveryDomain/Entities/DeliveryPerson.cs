using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Delivery.Domain.Entities
{
    public class DeliveryPerson
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        //public string Vehicle { get;  set; }
        public ICollection<Deliveryx> Deliveries { get; set; } = new List<Deliveryx>();

        public DeliveryPerson()
        {
        }
        public DeliveryPerson(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del repartidor no puede estar vacío.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
           // Vehicle = vehicle;
        }
       
    }
}
