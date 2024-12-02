namespace ProyectoViajes.API.Dtos.Assessments
{
    public class AssessmentDto
    {
        public Guid Id { get; set; }

        public int Stars { get; set; }

        public string Comment { get; set; }

        public string UserId { get; set; }

        public Guid TravelPackageId { get; set; }
    }
}