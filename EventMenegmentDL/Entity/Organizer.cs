namespace EventMenegmentDL.Entity
{

    public class Organizer : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public int EventId { get; set; }
        public List<Event> Events { get; set; }
    }
}
