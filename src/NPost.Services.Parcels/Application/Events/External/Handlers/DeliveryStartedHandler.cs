using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using NPost.Services.Parcels.Core.Repositories;

namespace NPost.Services.Parcels.Application.Events.External.Handlers
{
    public class DeliveryStartedHandler : IEventHandler<DeliveryStarted>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly ILogger<DeliveryStartedHandler> _logger;

        public DeliveryStartedHandler(IParcelsRepository parcelsRepository, ILogger<DeliveryStartedHandler> logger)
        {
            _parcelsRepository = parcelsRepository;
            _logger = logger;
        }

        public async Task HandleAsync(DeliveryStarted @event)
        {
            var parcel = await _parcelsRepository.GetAsync(@event.ParcelId);
            if (parcel is null)
            {
                throw new InvalidOperationException($"Parcel with id: {@event.ParcelId} was not found.");
            }

            parcel.StartDelivery();
            await _parcelsRepository.UpdateAsync(parcel);
            _logger.LogInformation($"The delivery for parcel with id: {@event.ParcelId} has started.");
        }
    }
}