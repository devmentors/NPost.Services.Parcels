using System;

namespace NPost.Services.Parcels.Application
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}