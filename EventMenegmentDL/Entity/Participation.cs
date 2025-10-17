namespace EventMenegmentDL.Entity
{
    public class Participation : BaseEntity
    {
       
            public int Id { get; set; }
            public string UserId { get; set; }
            public int InvitationId { get; set; } 
            public DateTime CheckInTime { get; set; }
            public int SeatNumber { get; set; }
            public AppUser User { get; set; }
            public Invitation Invitation { get; set; }
        

    }

}
