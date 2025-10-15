using EventMenegmentDL.Entity;

namespace EventMenegmentSL.ViewModel
{
    public class OrganizerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string ImageUrl { get; set; }
        public int IventId { get; set; }
        public List<EventViewModel> Events { get; set; }
    }
}
