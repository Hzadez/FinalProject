using EventMenegmentSL.ViewModel;

namespace EventMenegmentUI.Models
{
    public class HomeViewModel
    {
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
        public List<LocationViewModel> Locations { get; set; } = new List<LocationViewModel>();
        public List<EventTypeViewModel> EventTypes { get; set; } = new List<EventTypeViewModel>();
    }
}
