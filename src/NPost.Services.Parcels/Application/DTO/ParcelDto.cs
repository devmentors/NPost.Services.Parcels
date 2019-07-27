using System;
using NPost.Services.Parcels.Core.Entities;

namespace NPost.Services.Parcels.Application.DTO
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }

        public ParcelDto()
        {
        }

        public ParcelDto(Parcel parcel)
        {
            Id = parcel.Id;
            Name = parcel.Name;
            Size = parcel.Size.ToString();
            Status = parcel.Status.ToString();
        }
    }
}