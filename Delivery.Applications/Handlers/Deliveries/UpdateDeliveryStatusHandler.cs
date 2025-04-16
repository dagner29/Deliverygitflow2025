using Delivery.Applications.UsesCases.Deliveries;
//using Delivery.Applications.UsesCases.Interfaces;
using Delivery.Applications.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;

namespace Delivery.Applications.Handlers.Deliveries
{
        public class UpdateDeliveryStatusHandler
    {
        private readonly IRepository<Deliveryx> _repository;

        public UpdateDeliveryStatusHandler(IRepository<Deliveryx> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(UpdateDeliveryStatusCommand command)
        {
            var delivery = await _repository.GetByIdAsync(command.DeliveryId);
            if (delivery == null) throw new Exception("Delivery not found.");
            // Logic for updating status goes here.
            await _repository.UpdateAsync(delivery);
        }
    }

}
