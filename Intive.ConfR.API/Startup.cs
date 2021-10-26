using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Intive.ConfR.API.Extensions;
using Intive.ConfR.API.Middlewares;
using Intive.ConfR.Infrastructure.Hubs;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Hangfire;
using Intive.ConfR.API.Filters;
using Intive.ConfR.Logging;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Intive.ConfR.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            LoggerManager.Initialize(configuration, env);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddKeysFromSettings(Configuration)
                .AddAutoMapper()
                .AddFrameworkServices()
                .AddDbContexts(Configuration)
                .AddMediatR()
                .AddLogger()
                .AddSwagger()
                .AddAuthentications(Configuration)
                .AddCustomApiBehavior()
                .AddSignalRService()
                .AddImageApi()
                .ConfigureHealthChecks(Configuration)
                .AddHangfireService(Configuration)
                .BuildMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHealthChecks("/healthz", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(setup =>
            {
                setup.UIPath = "/health"; // UI path in the browser
                setup.ApiPath = "/health-ui-api"; // the UI (spa app) use this path to get information from the store ( this is NOT the healthz path, is internal ui api )
            });


            // Middlewares
            app.UseWhen(
                context => ((context.Request.Path.StartsWithSegments("/api/rooms", out var remaining) &&
                             (remaining.Value.Contains("reservations") || remaining.Value.Contains("availability"))) ||
                            context.Request.Path.StartsWithSegments("/api/reservations")),
                appBuilder => appBuilder.UseMiddleware<AccessTokenCheckerMiddleware>()
            );
            app.UseWhen(
                context => (context.Request.Path.StartsWithSegments("/api") ||
                            context.Request.Path.StartsWithSegments("/confr")),
                appBuilder => appBuilder.UseMiddleware<KeyValidatorMiddleware>()
            );

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSignalR(routes =>
                {
                    routes.MapHub<ConfrHub>("/confrhub");
                }
            );

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet ConfR");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
            });

            app.UseLoggingMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

        }
    }
}
