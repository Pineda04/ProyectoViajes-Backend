using ProyectoViajes.API.Dtos.PointsInterest;

namespace ProyectoViajes.API.Dtos.Destinations
{
    public class DestinationDto
    {
        // Id
        public Guid Id { get; set; }

        // Nombre
        public string Name { get; set; }

        // Descripción
        public string Description { get; set; }

        // Ubicación
        public string Location { get; set; }

        // Imagen
        public string ImageUrl { get; set; }

        // Punto de interés
        public IEnumerable<PointInterestDto> PointsInterest { get; set; }
    }
}