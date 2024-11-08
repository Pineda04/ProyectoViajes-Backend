using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Helpers;
using ProyectoViajes.API.Services;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add DbContext
            services.AddDbContext<ProyectoViajesContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"))
            );

            // Add Custom Services
            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IDestinationsService, DestinationsService>();
            services.AddTransient<IPointsInterestService, PointsInterestService>();
            services.AddTransient<ITravelPackagesService, TravelPackagesService>();
            services.AddTransient<IActivitiesService, ActivitiesService>();
            services.AddTransient<IHostingsService, HostingsService>();
            services.AddTransient<ITypesHostingService, TypesHostingService>();
            services.AddTransient<ITypesFlightService, TypesFlightService>();
            services.AddTransient<IFlightsService, FlightsService>();
            services.AddTransient<IAssessmentsService, AssessmentsService>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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