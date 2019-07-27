using System;
using System.Threading.Tasks;
using NPost.Services.Parcels.Core.Entities;

namespace NPost.Services.Parcels.Core.Repositories
{
    public interface IParcelsRepository
    {
        Task<Parcel> GetAsync(Guid id);
        Task AddAsync(Parcel parcel);
        Task UpdateAsync(Parcel parcel);
        Task DeleteAsync(Guid id);
    }
}