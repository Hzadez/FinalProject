namespace EventMenegmentDL.Entity
{

    public class UserInvitation : BaseEntity
    {



        public int Id { get; set; }

        public int InvitationId { get; set; }
        public Invitation Invitation { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public DateTime SentAt { get; set; }
        public InvitationStatus IsAccepted { get; set; } =InvitationStatus.Pending;

    }



}
