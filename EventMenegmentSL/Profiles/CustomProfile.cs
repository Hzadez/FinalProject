

using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Profiles
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<Event, EventViewModel>().ReverseMap();
            CreateMap<EventType, EventTypeViewModel>().ReverseMap();
            CreateMap<Feedback, FeedbackViewModel>().ReverseMap();
            CreateMap<Invitation, InvitationViewModel>().ReverseMap();
            CreateMap<Location, LocationViewModel>().ReverseMap();
            CreateMap<Notification, NotificationViewModel>().ReverseMap();
            CreateMap<Organizer, OrganizerViewModel>().ReverseMap();

            CreateMap<Participation, ParticipationViewModel>().ReverseMap();
            CreateMap<UserInvitation, UserInvitationViewModel>().ReverseMap();
            CreateMap<Participation, ParticipationViewModel>().ReverseMap();




        }
    }
}
