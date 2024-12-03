namespace ProyectoViajes.API.Dtos.PointsInterest
{
    public class PointInterestDto
    {
        // Id
        public Guid Id { get; set; }

        // Nombre
        public string Name { get; set; }

        // Descripción
        public string Description { get; set; }

        // Id del destino
        public Guid DestinationId { get; set; }

        // Imagen url
        public string ImageUrl { get; set; }
    }
}