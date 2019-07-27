using System;
using Convey.CQRS.Events;

namespace NPost.Services.Parcels.Application.Events
{
    [Contract]
    public class ParcelDeleted : IEvent
    {
        public Guid ParcelId { get; }

        public ParcelDeleted(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}