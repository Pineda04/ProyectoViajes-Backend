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

        private static async Task LoadTypesFlightsAsync(ProyectoViajesContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePatch = "SeedData/types_flights.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePatch);
                var types_flights = JsonConvert.DeserializeObject<List<TypeFlightEntity>>(jsonContent);

                if(!await context.TypesFlight.AnyAsync())
                {
                    for(int i=0; i < types_flights.Count; i++)
                    {
                        types_flights[i].CreatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
                        types_flights[i].CreatedDate = DateTime.Now;
                        types_flights[i].UpdatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
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
                    for(int i=0; i < types_hostings.Count; i++)
                    {
                        types_hostings[i].CreatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
                        types_hostings[i].CreatedDate = DateTime.Now;
                        types_hostings[i].UpdatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
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
                    for(int i=0; i < flights.Count; i++)
                    {
                        flights[i].CreatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
                        flights[i].CreatedDate = DateTime.Now;
                        flights[i].UpdatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
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
                    for(int i=0; i < hostings.Count; i++)
                    {
                        hostings[i].CreatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
                        hostings[i].CreatedDate = DateTime.Now;
                        hostings[i].UpdatedBy = "2a373bd7-1829-4bb4-abb7-19da4257891d";
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