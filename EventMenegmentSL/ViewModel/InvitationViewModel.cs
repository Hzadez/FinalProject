using EventMenegmentDL.Entity;

namespace EventMenegmentSL.ViewModel
{
    public class InvitationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public IEnumerable<EventViewModel>? Events { get; set; }
        public List<AppUser>? Users { get; set; }= new List<AppUser>();
        public List<string>? SelectedUserIds { get; set; } = new List<string>();
    }
   

}
