using NPost.Services.Parcels.Core.Entities;

namespace NPost.Services.Parcels.Application.DTO
{
    public class ParcelDetailsDto : ParcelDto
    {
        public string Address { get; set; }
        public string Notes { get; set; }

        public ParcelDetailsDto()
        {
        }

        public ParcelDetailsDto(Parcel parcel) : base(parcel)
        {
            Address = parcel.Address;
            Notes = parcel.Notes;
        }
    }
}