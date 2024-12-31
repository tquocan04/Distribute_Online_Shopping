namespace DTOs.DTOs
{
    public class RegionDTO
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<CityDTO>? Cities { get; set; }
    }
}
