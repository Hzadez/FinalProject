namespace EventMenegmentAdmin.DTO
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }

        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
