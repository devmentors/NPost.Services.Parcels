using System;
using Convey.CQRS.Commands;

namespace NPost.Services.Parcels.Application.Commands
{
    [Contract]
    public class AddParcel : ICommand
    {
        public Guid ParcelId { get; }
        public string Size { get; }
        public string Name { get; }
        public string Address { get; }

        public AddParcel(Guid parcelId, string size, string name, string address)
        {
            ParcelId = parcelId == Guid.Empty ? Guid.NewGuid() : parcelId;
            Size = size;
            Name = name;
            Address = address;
        }
    }
}