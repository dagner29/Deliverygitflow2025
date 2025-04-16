using Delivery.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Applications.UsesCases.Deliveries;
using Delivery.Applications.UsesCases.Packages;

namespace Delivery.Applications.Handlers.Packages
{
    public class AddPackageToDeliveryHandler
    {
        private readonly IRepository<Deliveryx> _repository;

        public AddPackageToDeliveryHandler(IRepository<Deliveryx> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(AddPackageToDeliveryCommand command)
        {
            var delivery = await _repository.GetByIdAsync(command.DeliveryId);
            if (delivery == null)
                throw new KeyNotFoundException("Delivery not found.");

            // Verifica que command.Package no sea nulo
            if (command.Package == null)
                throw new ArgumentException("Package cannot be null.");

            // Agrega el paquete
            delivery.AddPackage(command.Package);

            // Guarda los cambios
            await _repository.UpdateAsync(delivery);
        }
    }
 
}
