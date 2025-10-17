using EventMenegmentDL.Entity;

namespace EventMenegmentSL.ViewModel
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int EventTypeId { get; set; }
        public string ImageUrl { get; set; }
        public int LocationId { get; set; }
        public int OrganizerId { get; set; }
        public List<AppUser> Participants { get; set; } = new List<AppUser>();
        public EventType? EventType { get; set; }
        public Location? Location { get; set; }
        public Organizer? Organizer { get; set; }
    }
}
