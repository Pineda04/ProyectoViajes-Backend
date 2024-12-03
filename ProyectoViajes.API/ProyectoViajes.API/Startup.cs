using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            services.AddHttpContextAccessor();

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
            services.AddTransient<IReservationsService, ReservationsService>();
            services.AddTransient<IAuditService, AuditService>();

            // Add Identity
            services.AddIdentity<IdentityUser, IdentityRole>(options => 
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ProyectoViajesContext>()
              .AddDefaultTokenProviders();
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // CORS Configuration
            services.AddCors(opt =>
                {
                    var allowURLS = Configuration.GetSection("AllowURLS").Get<string[]>();

                    opt.AddPolicy("CorsPolicy", builder => builder
                        .WithOrigins(allowURLS)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                }
            );
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

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}