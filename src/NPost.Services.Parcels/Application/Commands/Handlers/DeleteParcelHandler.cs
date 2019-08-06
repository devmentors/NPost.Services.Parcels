using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using NPost.Services.Parcels.Application.Events;
using NPost.Services.Parcels.Core.Entities;
using NPost.Services.Parcels.Core.Repositories;

namespace NPost.Services.Parcels.Application.Commands.Handlers
{
    public class DeleteParcelHandler : ICommandHandler<DeleteParcel>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<DeleteParcelHandler> _logger;

        public DeleteParcelHandler(IParcelsRepository parcelsRepository, IMessageBroker messageBroker,
            ILogger<DeleteParcelHandler> logger)
        {
            _parcelsRepository = parcelsRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteParcel command)
        {
            var parcel = await _parcelsRepository.GetAsync(command.ParcelId);
            if (parcel is null)
            {
                throw new Exception($"Parcel with id: {command.ParcelId} was not found.");
            }

            if (parcel.Status != Status.New)
            {
                throw new InvalidOperationException($"Parcel with id: {command.ParcelId} " +
                                                    $"and status: {parcel.Status} cannot be deleted.");
            }

            await _parcelsRepository.DeleteAsync(command.ParcelId);
            await _messageBroker.PublishAsync(new ParcelDeleted(command.ParcelId));
            _logger.LogInformation($"Deleted a parcel with id: {command.ParcelId}");
        }
    }
}