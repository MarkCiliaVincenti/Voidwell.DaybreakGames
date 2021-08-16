﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voidwell.DaybreakGames.App.CensusStream.EventProcessors;
using Voidwell.DaybreakGames.CensusStore;
using Voidwell.DaybreakGames.CensusStream;
using Voidwell.DaybreakGames.HostedServices;
using Voidwell.DaybreakGames.HttpAuthenticatedClient;
using Voidwell.DaybreakGames.Services;
using Voidwell.DaybreakGames.Services.Planetside;

namespace Voidwell.DaybreakGames.App
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<DaybreakGamesOptions>(configuration);
            services.Configure<DaybreakGamesOptions>(options =>
            {
                var eventNames = configuration.GetValue<string>("CensusWebsocketServices");
                var experienceIds = configuration.GetValue<string>("CensusWebsocketExperienceIds");

                options.CensusWebsocketServices = eventNames?.Replace(" ", "").Split(",");

                if (experienceIds != null)
                {
                    options.CensusWebsocketExperienceIds = experienceIds.Replace(" ", "").Split(",");
                }
            });

            services.AddAuthenticatedHttpClient(options =>
            {
                options.TokenServiceAddress = "http://voidwellauth:5000/connect/token";
                options.ClientId = configuration.GetValue<string>("ClientId");
                options.ClientSecret = configuration.GetValue<string>("ClientSecret");
                options.Scopes = new List<string>
                    {
                        "voidwell-messagewell-publish"
                    };
            });

            services.AddCensusServices(options =>
            {
                options.CensusServiceId = configuration.GetValue<string>("CensusServiceKey");
                options.CensusServiceNamespace = configuration.GetValue<string>("CensusServiceNamespace");
                options.LogCensusErrors = configuration.GetValue<bool>("LogCensusErrors", false);
            });

            services.AddCensusStores(configuration);
            services.AddEventProcessors();

            services.AddTransient<IFeedService, FeedService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<IWeaponService, WeaponService>();
            services.AddTransient<IAlertService, AlertService>();
            services.AddTransient<ICombatReportService, CombatReportService>();
            services.AddTransient<IMetagameEventService, MetagameEventService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IGradeService, GradeService>();

            services.AddSingleton<ICharacterService, CharacterService>();
            services.AddSingleton<ILeaderboardService, LeaderboardService>();
            services.AddSingleton<ICharacterSessionService, CharacterSessionService>();
            services.AddSingleton<IOutfitService, OutfitService>();
            services.AddSingleton<IWorldMonitor, WorldMonitor>();
            services.AddSingleton<IPlayerMonitor, PlayerMonitor>();
            services.AddSingleton<IWorldService, WorldService>();
            services.AddSingleton<IWeaponAggregateService, WeaponAggregateService>();
            services.AddSingleton<IPSBUtilityService, PSBUtilityService>();
            services.AddSingleton<ICharacterRatingService, CharacterRatingService>();
            services.AddSingleton<IMapService, MapService>();
            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IWorldEventsService, WorldEventsService>();

            services.AddSingleton<ICharacterUpdaterService, CharacterUpdaterService>();
            services.AddSingleton<IWebsocketEventHandler, WebsocketEventHandler>();
            services.AddSingleton<IWebsocketMonitor, WebsocketMonitor>();
            services.AddSingleton<IWebsocketHealthMonitor, WebsocketHealthMonitor>();
            services.AddSingleton<IEventValidator, EventValidator>();
            services.AddSingleton<IEventProcessorHandler, EventProcessorHandler>();

            services.AddHostedService<WebsocketMonitorHostedService>();
            services.AddHostedService<CharacterUpdaterHostedService>();

            return services;
        }

        private static void AddEventProcessors(this IServiceCollection services)
        {
            typeof(IEventProcessor<>).GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .SelectMany(t => t.GetInterfaces().Select(i => (t, i)))
                .Where(a => a.i.IsGenericType && typeof(IEventProcessor<>).IsAssignableFrom(a.i.GetGenericTypeDefinition()))
                .ToList()
                .ForEach(a => services.AddSingleton(a.i, a.t));
        }
    }
}
