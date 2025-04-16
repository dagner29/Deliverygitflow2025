
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Applications.Interfaces;

 namespace Delivery.Applications.Interfaces
{
    public interface IDeliveryRepository : IRepository<Deliveryx>
    {
        Task<IEnumerable<Deliveryx>> GetDeliveriesByDateAsync(DateTime date);
        Task AssignDeliveryPersonAsync(Guid deliveryId, Guid deliveryPersonId);
    }
    
}
