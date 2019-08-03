using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using NPost.Services.Parcels.Core.Repositories;

namespace NPost.Services.Parcels.Application.Events.External.Handlers
{
    public class DeliveryFailedHandler : IEventHandler<DeliveryFailed>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IAppContext _appContext;
        private readonly ILogger<DeliveryFailedHandler> _logger;

        public DeliveryFailedHandler(IParcelsRepository parcelsRepository, IAppContext appContext, ILogger<DeliveryFailedHandler> logger)
        {
            _parcelsRepository = parcelsRepository;
            _appContext = appContext;
            _logger = logger;
        }

        public async Task HandleAsync(DeliveryFailed @event)
        {
            var parcel = await _parcelsRepository.GetAsync(@event.ParcelId);
            if (parcel is null)
            {
                throw new InvalidOperationException($"Parcel with id: {@event.ParcelId} was not found.");
            }

            parcel.FailDelivery(@event.Reason);
            await _parcelsRepository.UpdateAsync(parcel);
            _logger.LogInformation($"The delivery for parcel with id: {@event.ParcelId} has failed " +
                                   $"reason: {@event.Reason}.");
        }
    }
}