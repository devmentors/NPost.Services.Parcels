using System;

namespace NPost.Services.Parcels.Core.Entities
{
    public class Parcel
    {
        public Guid Id { get; private set; }
        public Size Size { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public Status Status { get; private set; }
        public string Notes { get; private set; }

        public Parcel(Guid id, Size size, string name, string address)
        {
            Id = id;
            Size = size;
            Address = address;
            Name = string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("Missing parcel name.", nameof(name))
                : name;
            Address = string.IsNullOrWhiteSpace(address)
                ? throw new ArgumentException("Missing parcel address.", nameof(address))
                : address;
            Status = Status.New;
            Notes = string.Empty;
        }

        public void StartDelivery()
            => TryChangeStatus(Status.InDelivery, () => Status == Status.New || Status == Status.DeliveryFailed);

        public void CompleteDelivery() => TryChangeStatus(Status.Delivered, () => Status == Status.InDelivery);

        public void FailDelivery(string reason)
        {
            TryChangeStatus(Status.DeliveryFailed, () => Status != Status.Delivered);
            Notes = reason ?? string.Empty;
        }

        private void TryChangeStatus(Status status, Func<bool> validator)
        {
            if (!validator())
            {
                throw new InvalidOperationException($"Package status cannot be changed to: {status}");
            }

            Status = status;
        }
    }
}