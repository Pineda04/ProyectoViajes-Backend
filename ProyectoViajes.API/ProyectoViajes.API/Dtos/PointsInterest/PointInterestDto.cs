namespace ProyectoViajes.API.Dtos.PointsInterest
{
    public class PointInterestDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid DestinationId { get; set; }
    }
}