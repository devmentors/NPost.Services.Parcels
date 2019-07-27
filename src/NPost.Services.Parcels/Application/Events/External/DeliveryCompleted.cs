using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace NPost.Services.Parcels.Application.Events.External
{
    [MessageNamespace("deliveries")]
    public class DeliveryCompleted : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid ParcelId { get; }

        public DeliveryCompleted(Guid deliveryId, Guid parcelId)
        {
            DeliveryId = deliveryId;
            ParcelId = parcelId;
        }
    }
}