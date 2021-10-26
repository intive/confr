using AutoMapper;
using Intive.ConfR.API.Filters;
using Intive.ConfR.API.Middlewares;
using Intive.ConfR.Application.Infrastructure;
using Intive.ConfR.Application.Infrastructure.AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using Intive.ConfR.Application.Rooms.Queries.GetRoomsList;
using Intive.ConfR.Common;
using Intive.ConfR.Infrastructure;
using Intive.ConfR.Infrastructure.AzureStorage;
using Intive.ConfR.Infrastructure.AzureStorage.Helpers;
using Intive.ConfR.Logging;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;
using Intive.ConfR.Application.Rooms.Commands.CreateRoom;
using Intive.ConfR.Infrastructure.Hubs;
using Intive.ConfR.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Hangfire;
using Intive.ConfR.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Intive.ConfR.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConfRContext>(o => o.UseSqlServer(configuration.GetConnectionString("ConfRDatabase")));

            return services;
        }

        public static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IRoomsDirectory, MicrosoftGraphRoomsApi>();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAzureStorageContainerService, AzureStorageContainerService>();
            services.AddTransient<IAzureStorageImageService, AzureStorageImageService>();
            services.AddTransient<IConfrHubService, ConfrHubService>();
            services.AddTransient<ICommentService, CommentService>();

            return services;
        }

        public static IServiceCollection AddImageApi(this IServiceCollection services)
        {
            services.AddTransient<IStorageConnectionHelper, StorageConnectionHelper>();
            services.AddTransient<INameGenerator, NameGenerator>();
            services.AddTransient<IImageProcessingService, ImageProcessingService>();

            return services;
        }

        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            services.AddTransient<ILoggerManager, LoggerManager>();

            return services;
        }

        public static IServiceCollection AddKeysFromSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppKeys>(configuration.GetSection("AppKeys"));
            services.Configure<AcceptedExtensions>(configuration.GetSection("PhotoStorage:AcceptedExtensions"));
            services.Configure<ThumbnailDimensions>(configuration.GetSection("PhotoStorage:ThumbnailDimensions"));
            services.Configure<StorageConnectionString>(configuration.GetSection("PhotoStorage:ConnectionData"));
            services.Configure<AuthorizationData>(configuration.GetSection("AuthorizationData"));
            services.Configure<Mails>(options => configuration.GetSection("Mails").Bind(options));

            return services;
        }

        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetRoomsListQuery).GetTypeInfo().Assembly);
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));

            return services;
        }

        public static IServiceCollection AddAuthentications(this IServiceCollection services, IConfiguration configuration)
        {

            if (Convert.ToBoolean(configuration["AuthorizationEnabled"]))
            {
                // OpenId Connect metadata
                var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    configuration["OpenIdConnectMetadata"],
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever());
                var openIdConfig = configurationManager.GetConfigurationAsync().Result;

                // JWT data
                var listOfKeys = openIdConfig.SigningKeys;
                var listOfIssuers = configuration.GetSection("JWT:Issuers").Get<List<string>>();

                // JWT authentication
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            RequireExpirationTime = true,
                            RequireSignedTokens = false,
                            ClockSkew = TimeSpan.FromMinutes(5),

                            ValidIssuers = listOfIssuers,
                            IssuerSigningKeys = listOfKeys

                        };
                    });
            }
            else
            {
                // Fake authentication
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "FakeScheme";
                        options.DefaultChallengeScheme = "FakeScheme";
                    })
                    .AddFakeAuth(options => { });
            }

            return services;
        }

        public static IServiceCollection AddHangfireService(this IServiceCollection services, IConfiguration configuration)
        {
            var x = configuration.GetConnectionString("HangfireConnection");

            services.AddHangfire(config =>
             config.UseSqlServerStorage(x));

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ConfR", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert JWT id token with Bearer into field. Example: \"Bearer id_token\"",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.AddSecurityDefinition("Access_token", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert graph jwt access token",
                    Name = "Access_token",
                    Type = "apiKey"
                });

                c.AddSecurityDefinition("App-Key", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert app key (guid)",
                    Name = "App-Key",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } },
                    { "Access_token", new string[] { } },
                    { "App-Key", new string[] { } },
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomApiBehavior(this IServiceCollection services)
        {
            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IServiceCollection BuildMvc(this IServiceCollection services)
        {
            services
            .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRoomCommandValidator>());

            return services;
        }

        public static IServiceCollection AddSignalRService(this IServiceCollection services)
        {
            services.AddSignalR();

            return services;
        }

        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            StorageConnectionString storageConfig = configuration.GetSection("PhotoStorage:ConnectionData").Get<StorageConnectionString>();

            services.AddHealthChecksUI()
                .AddHealthChecks()
                .AddCheck("self", checks =>
                {
                    return HealthCheckResult.Healthy();
                })
                .AddSqlServer(configuration.GetConnectionString("ConfRDatabase"), name: "SQL Server")
                .AddAzureBlobStorage(storageConfig.ConnectionString, name: "Azure Blob Storage");
                
            return services;
        }
    }
}
