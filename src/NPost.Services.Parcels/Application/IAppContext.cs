using System.Collections.Generic;

namespace NPost.Services.Parcels.Application
{
    public interface IAppContext
    {
        string RequestId { get; }
        string UserId { get; }
        string Role { get; }
        IDictionary<string, string> Claims { get; }
    }
}