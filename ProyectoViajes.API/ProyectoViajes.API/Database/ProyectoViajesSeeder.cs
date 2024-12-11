using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database.Entities;

namespace ProyectoViajes.API.Database
{
    public class ProyectoViajesSeeder
    {
        public static async Task LoadDataAsync(
            ProyectoViajesContext context, 
            ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            try
            {
                await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);
                await LoadDestinationsAsync(context, loggerFactory);
                await LoadPointsInterestAsync(context, loggerFactory);
                await LoadTravelPackagesAsync(context, loggerFactory);
                await LoadActivitiesAsync(context, loggerFactory);
                await LoadTypesFlightsAsync(context, loggerFactory);
                await LoadTypesHostingsAsync(context, loggerFactory);
                await LoadFlightsAsync(context, loggerFactory);
                await LoadHostingsAsync(context, loggerFactory);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al iniciar la data del API");
            }
        }

        private static async Task LoadRolesAndUsersAsync(
            UserManager<UserEntity> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ILoggerFactory loggerFactory
        )
        {
            try 
            {
                if (!await roleManager.Roles.AnyAsync()) 
                {
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN)); 
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.USER)); 
                }

                if (!await userManager.Users.AnyAsync()) 
                {
                    var userAdmin = new UserEntity 
                    {
                        FirstName = "Administrador",
                        LastName = "Proyecto Viajes",
                        Email = "admin@proyecto.viajes",
                        UserName = "admin@proyecto.viajes",                        
                    };
                    
                    var normalUser = new UserEntity
                    {
                        FirstName = "Usuario",
                        LastName = "Proyecto Viajes",
                        Email = "user@proyecto.viajes",
                        UserName = "user@proyecto.viajes",
                    };

                    await userManager.CreateAsync(userAdmin, "Temporal01*");
                    await userManager.CreateAsync(normalUser, "Temporal01*");

                    await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);
                    await userManager.AddToRoleAsync(normalUser, RolesConstant.USER);
                }
            } 
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(e.Message);
            }
        }

        private static async Task LoadDestinationsAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePatch = "SeedData/destinations.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var destinations = JsonConvert.DeserializeObject<List<DestinationEntity>>(jsonContent);

                if (!await context.Destinations.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < destinations.Count; i++)
                    {
                        destinations[i].CreatedBy = user.Id;
                        destinations[i].CreatedDate = DateTime.Now;
                        destinations[i].UpdatedBy = user.Id;
                        destinations[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(destinations);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al ejecutar el seed de destinos");
            }
        }

        private static async Task LoadPointsInterestAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePatch = "SeedData/points_interest.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var points_interest = JsonConvert.DeserializeObject<List<PointInterestEntity>>(jsonContent);

                if (!await context.PointsInterest.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < points_interest.Count; i++)
                    {
                        points_interest[i].CreatedBy = user.Id;
                        points_interest[i].CreatedDate = DateTime.Now;
                        points_interest[i].UpdatedBy = user.Id;
                        points_interest[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(points_interest);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al ejecutar el seed de puntos de interes");
            }
        }

        private static async Task LoadTravelPackagesAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try{
                var jsonFilePatch = "SeedData/travel_packages.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var travel_packages = JsonConvert.DeserializeObject<List<TravelPackageEntity>>(jsonContent);

                if(!await context.Travels.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for(int i=0; i < travel_packages.Count; i++)
                    {
                        travel_packages[i].CreatedBy = user.Id;
                        travel_packages[i].CreatedDate = DateTime.Now;
                        travel_packages[i].UpdatedBy = user.Id;
                        travel_packages[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(travel_packages);
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex){
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al ejecutar el seed de paquetes de viaje");
            }
        }

        private static async Task LoadActivitiesAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try{
                var jsonFilePatch = "SeedData/activities.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var activities = JsonConvert.DeserializeObject<List<ActivityEntity>>(jsonContent);

                if(!await context.Activities.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for(int i=0; i < activities.Count; i++)
                    {
                        activities[i].CreatedBy = user.Id;
                        activities[i].CreatedDate = DateTime.Now;
                        activities[i].UpdatedBy = user.Id;
                        activities[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(activities);
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex){
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al ejecutar el seed de actividades");
            }
        }

        private static async Task LoadTypesFlightsAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePatch = "SeedData/types_flights.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var types_flights = JsonConvert.DeserializeObject<List<TypeFlightEntity>>(jsonContent);

                if(!await context.TypesFlight.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for(int i=0; i < types_flights.Count; i++)
                    {
                        types_flights[i].CreatedBy = user.Id;
                        types_flights[i].CreatedDate = DateTime.Now;
                        types_flights[i].UpdatedBy = user.Id;
                        types_flights[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(types_flights);
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al ejecutar el seed de tipos de vuelo");
            }
        }

        private static async Task LoadTypesHostingsAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePatch = "SeedData/types_hostings.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var types_hostings = JsonConvert.DeserializeObject<List<TypeHostingEntity>>(jsonContent);

                if(!await context.TypesHosting.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for(int i=0; i < types_hostings.Count; i++)
                    {
                        types_hostings[i].CreatedBy = user.Id;
                        types_hostings[i].CreatedDate = DateTime.Now;
                        types_hostings[i].UpdatedBy = user.Id;
                        types_hostings[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(types_hostings);
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al ejecutar el seed de tipos de alojamientos");
            }
        }

        private static async Task LoadFlightsAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePatch = "SeedData/flights.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var flights = JsonConvert.DeserializeObject<List<FlightEntity>>(jsonContent);

                if(!await context.Flights.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for(int i=0; i < flights.Count; i++)
                    {
                        flights[i].CreatedBy = user.Id;
                        flights[i].CreatedDate = DateTime.Now;
                        flights[i].UpdatedBy = user.Id;
                        flights[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(flights);
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al ejecutar el seed de vuelos");
            }
        }

        private static async Task LoadHostingsAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePatch = "SeedData/hostings.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var hostings = JsonConvert.DeserializeObject<List<HostingEntity>>(jsonContent);

                if(!await context.Hostings.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for(int i=0; i < hostings.Count; i++)
                    {
                        hostings[i].CreatedBy = user.Id;
                        hostings[i].CreatedDate = DateTime.Now;
                        hostings[i].UpdatedBy = user.Id;
                        hostings[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(hostings);
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al ejecutar el seed de alojamientos");
            }
        }
    }
}