using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NPost.Services.Parcels.Application.DTO;
using NPost.Services.Parcels.Application.Queries;
using NPost.Services.Parcels.Core.Entities;

namespace NPost.Services.Parcels.Infrastructure.Mongo.Queries
{
    public class GetParcelsHandler : IQueryHandler<GetParcels, IEnumerable<ParcelDto>>
    {
        private readonly IMongoDatabase _database;

        public GetParcelsHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<ParcelDto>> HandleAsync(GetParcels query)
        {
            var documents = _database.GetCollection<Parcel>("parcels").AsQueryable();
            if (Enum.TryParse<Size>(query.Size, true, out var size))
            {
                documents = documents.Where(p => p.Size == size);
            }

            if (Enum.TryParse<Status>(query.Status, true, out var status))
            {
                documents = documents.Where(p => p.Status == status);
            }

            var parcels = await documents.ToListAsync();

            return parcels.Select(p => new ParcelDto(p));
        }
    }
}