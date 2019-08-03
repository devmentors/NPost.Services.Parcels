using System.Threading.Tasks;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Microsoft.AspNetCore.Http;
using NPost.Services.Parcels.Application;

namespace NPost.Services.Parcels.Infrastructure.Services
{
    internal sealed class MessageBroker : IMessageBroker
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICorrelationContextAccessor _contextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MessageBroker(IBusPublisher busPublisher, ICorrelationContextAccessor contextAccessor,
            IHttpContextAccessor httpContextAccessor)
        {
            _busPublisher = busPublisher;
            _contextAccessor = contextAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task PublishAsync(params IEvent[] events)
        {
            if (events is null)
            {
                return;
            }

            foreach (var @event in events)
            {
                if (@event is null)
                {
                    continue;
                }

                await _busPublisher.PublishAsync(@event, _contextAccessor.CorrelationContext ??
                                                         _httpContextAccessor.GetCorrelationContext());
            }
        }
    }
}