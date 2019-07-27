using System.Collections.Generic;
using Convey.CQRS.Queries;
using NPost.Services.Parcels.Application.DTO;

namespace NPost.Services.Parcels.Application.Queries
{
    public class GetParcels : IQuery<IEnumerable<ParcelDto>>
    {
        public string Size { get; set; }
        public string Status { get; set; }
    }
}