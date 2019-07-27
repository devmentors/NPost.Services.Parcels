using System;
using Convey.CQRS.Queries;
using NPost.Services.Parcels.Application.DTO;

namespace NPost.Services.Parcels.Application.Queries
{
    public class GetParcel : IQuery<ParcelDetailsDto>
    {
        public Guid ParcelId { get; set; }
    }
}