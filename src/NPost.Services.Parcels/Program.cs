using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NPost.Services.Parcels.Application;
using NPost.Services.Parcels.Application.Commands;
using NPost.Services.Parcels.Application.DTO;
using NPost.Services.Parcels.Application.Queries;
using NPost.Services.Parcels.Infrastructure;

namespace NPost.Services.Parcels
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetParcel, ParcelDetailsDto>("parcels/{parcelId}")
                        .Get<GetParcels, IEnumerable<ParcelDto>>("parcels")
                        .Post<AddParcel>("parcels",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"parcels/{cmd.ParcelId}"))
                        .Delete<DeleteParcel>("parcels/{parcelId}")))
                .UseLogging()
                .Build()
                .RunAsync();
    }
}