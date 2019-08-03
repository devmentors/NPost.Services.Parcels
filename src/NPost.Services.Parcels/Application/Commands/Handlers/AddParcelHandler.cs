using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using NPost.Services.Parcels.Application.Events;
using NPost.Services.Parcels.Core.Entities;
using NPost.Services.Parcels.Core.Repositories;

namespace NPost.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IAppContext _appContext;
        private readonly ILogger<AddParcelHandler> _logger;

        public AddParcelHandler(IParcelsRepository parcelsRepository, IMessageBroker messageBroker,
            IAppContext appContext, ILogger<AddParcelHandler> logger)
        {
            _parcelsRepository = parcelsRepository;
            _messageBroker = messageBroker;
            _appContext = appContext;
            _logger = logger;
        }
        
        public async Task HandleAsync(AddParcel command)
        {
            if (!Enum.TryParse<Size>(command.Size, true, out var size))
            {
                throw new ArgumentException($"Invalid parcel size: {size}");
            }

            var parcel = new Parcel(command.ParcelId, size, command.Name, command.Address);
            await _parcelsRepository.AddAsync(parcel);
            await _messageBroker.PublishAsync(new ParcelAdded(command.ParcelId));
            _logger.LogInformation($"Added a parcel with id: {command.ParcelId}");
        }
    }
}