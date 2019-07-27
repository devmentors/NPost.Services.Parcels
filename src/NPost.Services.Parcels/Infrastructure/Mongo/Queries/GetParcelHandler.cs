using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using NPost.Services.Parcels.Application.DTO;
using NPost.Services.Parcels.Application.Queries;
using NPost.Services.Parcels.Core.Entities;

namespace NPost.Services.Parcels.Infrastructure.Mongo.Queries
{
    public class GetParcelHandler : IQueryHandler<GetParcel, ParcelDetailsDto>
    {
        private readonly IMongoDatabase _database;

        public GetParcelHandler(IMongoDatabase database)
        {
            _database = database;
        }
        
        public async Task<ParcelDetailsDto> HandleAsync(GetParcel query)
        {
            var parcel = await _database.GetCollection<Parcel>("parcels")
                .Find(p => p.Id == query.ParcelId)
                .SingleOrDefaultAsync();

            return parcel is null ? null : new ParcelDetailsDto(parcel);
        }
    }
}