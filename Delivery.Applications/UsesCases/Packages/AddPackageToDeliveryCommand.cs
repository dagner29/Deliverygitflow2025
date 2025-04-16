using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;

using MediatR;

namespace Delivery.Applications.UsesCases.Packages
{
    public class AddPackageToDeliveryCommand //: IRequest
    {
        public Guid DeliveryId { get; set; }
        public Package Package { get; set; }

        /* 
         public Guid DeliveryId { get; set; }
         public Guid PackageId { get; set; }
         public AddPackageToDeliveryCommand(Guid deliveryId, Guid packageId)
         {
             DeliveryId = deliveryId;
             PackageId = packageId;
         }
        */
    }
}
