
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Domain.ValueObjects;
using Delivery.Applications.Interfaces;
using MediatR;

namespace Delivery.Applications.UsesCases.Deliveries
{
    public class CreateDeliveryCommand //: IRequest<Guid>
    {
        public DateTime ScheduledDate { get; set; }
        public int DeliveryAddressid { get; set; }
        public int DeliveryRouteId { get; set; }

    }
}
