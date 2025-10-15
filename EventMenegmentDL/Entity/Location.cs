namespace EventMenegmentDL.Entity
{
    public class Location : BaseEntity
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public List<Event> Events { get; set; }
        public bool IsActive { get; set; } = true;

    }

}
