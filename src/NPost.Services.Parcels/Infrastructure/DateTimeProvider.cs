using System;
using NPost.Services.Parcels.Application;

namespace NPost.Services.Parcels.Infrastructure
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now  => DateTime.UtcNow;
    }
}