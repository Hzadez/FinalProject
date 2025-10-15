using EventMenegmentSL.ViewModel;

namespace EventMenegmentUI.Models
{
    public class LocationDetailViewModel
    {
        public LocationViewModel Location { get; set; }
        public List<EventViewModel> Events { get; set; }
    }
}
