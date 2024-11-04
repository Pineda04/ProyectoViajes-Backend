using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProyectoViajes.API.Database.Entities;

namespace ProyectoViajes.API.Database
{
    public class ProyectoViajesSeeder
    {
        public static async Task LoadDataAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await LoadDestinationsAsync(context, loggerFactory);
                await LoadPointsInterestAsync(context, loggerFactory);
                await LoadTravelPackagesAsync(context, loggerFactory);
                await LoadActivitiesAsync(context, loggerFactory);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ProyectoViajesSeeder>();
                logger.LogError(ex, "Error al iniciar la data del API");
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
                    for (int i = 0; i < destinations.Count; i++)
                    {
                        destinations[i].CreatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
                        destinations[i].CreatedDate = DateTime.Now;
                        destinations[i].UpdatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
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
                    for (int i = 0; i < points_interest.Count; i++)
                    {
                        points_interest[i].CreatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
                        points_interest[i].CreatedDate = DateTime.Now;
                        points_interest[i].UpdatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
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
                    for(int i=0; i < travel_packages.Count; i++)
                    {
                        travel_packages[i].CreatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
                        travel_packages[i].CreatedDate = DateTime.Now;
                        travel_packages[i].UpdatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
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
                    for(int i=0; i < activities.Count; i++)
                    {
                        activities[i].CreatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
                        activities[i].CreatedDate = DateTime.Now;
                        activities[i].UpdatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
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
    }
}