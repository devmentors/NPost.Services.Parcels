using System.Linq;
using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NPost.Services.Parcels.Application;
using NPost.Services.Parcels.Application.Events.External;
using NPost.Services.Parcels.Core.Repositories;
using NPost.Services.Parcels.Infrastructure.Contexts;
using NPost.Services.Parcels.Infrastructure.Mongo.Repositories;
using NPost.Services.Parcels.Infrastructure.Services;

namespace NPost.Services.Parcels.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IParcelsRepository, ParcelsMongoRepository>();
            builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
            builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq<CorrelationContext>()
                .AddMongo()
                .AddSwaggerDocs()
                .AddWebApiSwaggerDocs();
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseInitializers()
                .UsePublicContracts<ContractAttribute>()
                .UseConsul()
                .UseSwagger()
                .UseSwaggerUI()
                .UseSwaggerDocs()
                .UseRabbitMq()
                .SubscribeEvent<DeliveryStarted>()
                .SubscribeEvent<DeliveryCompleted>()
                .SubscribeEvent<DeliveryFailed>();

            return app;
        }

        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
            => accessor.HttpContext.Request.Headers.TryGetValue("Correlation-Context", out var json)
                ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault())
                : null;
    }
}