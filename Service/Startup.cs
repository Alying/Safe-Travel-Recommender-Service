namespace Service
{
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

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
