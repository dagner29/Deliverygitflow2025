using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Applications.Interfaces;
using MediatR;

namespace Delivery.Applications.UsesCases.Packages
{
    public class GetPackagesByDeliveryQuery
    {      
        public Guid DeliveryId { get; set; }
    }
}
