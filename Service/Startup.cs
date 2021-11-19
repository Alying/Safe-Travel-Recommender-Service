using Management;
using Management.Clients;
using Management.Interface;
using Management.Ports;
using Management.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage;
using Storage.Interface;

namespace Service
{
    /// <summary>
    /// Configure service and define http request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">configuration for the service.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration for service.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services that will be added to the container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Storage
            services.AddSingleton<IRepository, BaseRepository>();

            // Management
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IDecisionEngine, DecisionEngine>();
            services.AddScoped<ICovidDataClient, CovidDataClient>();
            services.AddScoped<IWeatherDataClient, WeatherDataClient>();
            services.AddScoped<IAirQualityDataClient, AirQualityDataClient>();

            // Ports
            services.AddScoped<CommentPort>();
            services.AddScoped<RecommendationPort>();

            services.AddControllers();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">the service app.</param>
        /// <param name="env">the web host environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
