namespace EventMenegmentDL.Entity
{
    public class Event : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public int EventTypeId { get; set; }
        public int LocationId { get; set; }
        public int OrganizerId { get; set; }
        public List<UserInvitation> Participants { get; set; } = new List<UserInvitation>();
        public EventType? EventType { get; set; }
        public Location? Location { get; set; }
        public Organizer? Organizer { get; set; }
    }

}
