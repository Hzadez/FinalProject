namespace EventMenegmentDL.Entity
{
    public class Feedback : BaseEntity
    {
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public DateTime SubmittedAt { get; set; }
        public Event Event { get; set; }
        public AppUser Person { get; set; }
    }

}
