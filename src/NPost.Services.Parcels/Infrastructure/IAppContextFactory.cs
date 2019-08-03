using NPost.Services.Parcels.Application;

namespace NPost.Services.Parcels.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}