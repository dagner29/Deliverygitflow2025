
using Delivery.Applications.UsesCases.Packages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Applications.Handlers.Packages
{
    public class GetPackagesByDeliveryQueryHandler
    {
        public Guid DeliveryId { get; set; }
    }
   
}
 