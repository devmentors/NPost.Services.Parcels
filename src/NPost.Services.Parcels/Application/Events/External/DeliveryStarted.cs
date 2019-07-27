using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace NPost.Services.Parcels.Application.Events.External
{
    [MessageNamespace("deliveries")]
    public class DeliveryStarted : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid ParcelId { get; }

        public DeliveryStarted(Guid deliveryId, Guid parcelId)
        {
            DeliveryId = deliveryId;
            ParcelId = parcelId;
        }
    }
}