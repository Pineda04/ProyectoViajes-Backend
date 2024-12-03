namespace ProyectoViajes.API.Dtos.Assessments
{
    public class AssessmentDto
    {
        // Id
        public Guid Id { get; set; }

        // Valoración
        public int Stars { get; set; }

        // Comentario
        public string Comment { get; set; }

        // Id del usuario
        public string UserId { get; set; }

        // Id del paquete de viaje
        public Guid TravelPackageId { get; set; }
    }
}