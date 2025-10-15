using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentAdmin.DTO
{
    public class OrganizerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
        public int IventId { get; set; }
        public List<EventViewModel>? Events { get; set; }
    }
}
