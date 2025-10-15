using EventMenegmentDL.Entity;

namespace EventMenegmentSL.ViewModel
{
    public class ParticipationViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int InvitationId { get; set; }  
        public DateTime CheckInTime { get; set; }
        public string LocationName { get; set; }
        public int SeatNumber { get; set; }
        public AppUser User { get; set; }
        public Invitation Invitation { get; set; }
    }

}
