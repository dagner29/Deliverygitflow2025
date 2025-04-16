using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities
{
    public class Administrator
    {
        //public Guid Id { get; private set; }
        //public string Name { get; private set; }
        //public List<Deliveryx> MonitoredDeliveries { get; private set; }

        //public Administrator(Guid id, string name)
        //{
        //    Id = id;
        //    Name = name;
        //    MonitoredDeliveries = new List<Deliveryx>();
        //}

        //public void MonitorDelivery(Deliveryx delivery)
        //{
        //    MonitoredDeliveries.Add(delivery);
        //}
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Administrator(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
