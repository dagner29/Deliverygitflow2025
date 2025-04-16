using Delivery.Applications.UsesCases.Deliveries;
using Delivery.Domain.Entities;
using Delivery.Applications.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Applications.Handlers.Deliveries
{
    public class AssignDeliveryToPersonHandler
    {
        private readonly IRepository<Deliveryx> _repository;
        private readonly IRepository<DeliveryPerson> _personRepository;

        public AssignDeliveryToPersonHandler(IRepository<Deliveryx> repository, IRepository<DeliveryPerson> personRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
        }

        public async Task HandleAsync(AssignDeliveryToPersonCommand command)
        {
            var delivery = await _repository.GetByIdAsync(command.DeliveryId);
            if (delivery == null) throw new Exception("Delivery not found.");

            var person = await _personRepository.GetByIdAsync(command.DeliveryPersonId);
            if (person == null) throw new Exception("Delivery person not found.");

            delivery.AssignDeliveryPerson(person);
            await _repository.UpdateAsync(delivery);
        }
    }

   }
