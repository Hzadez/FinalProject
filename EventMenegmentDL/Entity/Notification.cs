namespace EventMenegmentDL.Entity
{
    public class Notification : BaseEntity
    {
        public int PersonId { get; set; }
        public string Message { get; set; }
        public int EventId { get; set; }
        public DateTime SendAt { get; set; }
        public Event Event { get; set; }
    }

}
