namespace ApiProjeKampi.WebApi.Dtos.ResultServiceDtos
{
    public class ResultServiceDto
    {
        public int ServiceId { get; set; }
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string IconUrl { get; set; } = null!;
    }
}
