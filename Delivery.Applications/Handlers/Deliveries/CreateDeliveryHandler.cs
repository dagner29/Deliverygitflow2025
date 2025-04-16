using Delivery.Applications.UsesCases.Deliveries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Applications.Interfaces;
using Microsoft.Extensions.Logging;
using Delivery.Domain.ValueObjects;

namespace Delivery.Applications.Handlers.Deliveries
{

    public class CreateDeliveryHandler
    {
        private readonly IRepository<Deliveryx> _repository;
        private readonly ILogger<CreateDeliveryHandler> _logger;

        public CreateDeliveryHandler(IRepository<Deliveryx> repository, ILogger<CreateDeliveryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(CreateDeliveryCommand command)
        {
            _logger.LogInformation("Iniciando HandleAsync en CreateDeliveryHandler");

            var delivery = new Deliveryx (command.ScheduledDate, command.DeliveryAddressid, command.DeliveryRouteId);
            _logger.LogInformation("Creando delivery con fecha: {ScheduledDate}, dirección: {DeliveryAddress}",
            command.ScheduledDate, command.DeliveryAddressid);

            await _repository.AddAsync(delivery);
            _logger.LogInformation("Delivery guardado con éxito: {Id}", delivery.Id);

        }
    }



}
