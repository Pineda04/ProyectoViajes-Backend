namespace ProyectoViajes.API.Dtos.Hostings
{
    public class HostingDto
    {
        // Id
        public Guid Id { get; set; }

        // Id del hospedaje
        public Guid TypeHostingId { get; set; }  

        // Id del destino
        public Guid DestinationId { get; set; } 

        // Nombre
        public string Name { get; set; }

        // Descripción
        public string Description { get; set; }

        // Precio por noche
        public decimal PricePerNight { get; set; }
    }
}