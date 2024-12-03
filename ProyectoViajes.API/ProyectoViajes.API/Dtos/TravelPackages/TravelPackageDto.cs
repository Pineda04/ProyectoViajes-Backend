using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Assessments;
using ProyectoViajes.API.Dtos.Destinations;

namespace ProyectoViajes.API.Dtos.TravelPackages
{
    public class TravelPackageDto
    {
        // Id
        public Guid Id { get; set; }

        // Nombre
        public string Name { get; set; }

        // Descripción
        public string Description { get; set; }

        // Precio
        public decimal Price { get; set; }

        // Duración
        public int Duration { get; set; }

        // Numero de personas
        public int NumberPerson { get; set; }

        // Url de la imagen
        public string ImageUrl { get; set; }

        // Promedio de estrellas
        public double AverageStars { get; set; }

        // Lista de actividades
        public List<ActivityDto> Activities { get; set; }

        // Lista de valoraciones
        public List<AssessmentDto> Assessments { get; set; }

        // Para los destinos
        public DestinationDto Destinations { get; set; }
    }
}