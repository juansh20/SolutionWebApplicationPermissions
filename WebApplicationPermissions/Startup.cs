using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using WebApplicationPermissions.Context;
using WebApplicationPermissions.Interfaces;
using WebApplicationPermissions.Repositories;
using WebApplicationPermissions.Services;

namespace WebApplicationPermissions
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DefaultContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IElasticsearchService>(s => new ElasticsearchService(Configuration));
            services.AddSingleton<IKafkaProducerService>(s => new KafkaProducerService(Configuration.GetValue<string>("KafkaBootstrapServers")));

            services.AddScoped<IPermissionService, PermissionService>();

            services.AddControllers();

            services.AddSwaggerGen();

            //CORS
            string strUrlcors = Configuration.GetValue<string>("UrlsCors");

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    // loads the origins from AppSettings
                    foreach (var origin in strUrlcors.Split(','))
                    {
                        builder.WithOrigins(origin);
                    }

                    // builder.AllowAnyOrigin()
                    builder.AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials();
                });
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
                };
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
