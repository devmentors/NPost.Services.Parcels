using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace NPost.Services.Parcels.Application
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IEvent[] events);
    }
}