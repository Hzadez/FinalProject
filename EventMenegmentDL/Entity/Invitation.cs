namespace EventMenegmentDL.Entity
{
   
    public class Invitation : BaseEntity
    {
    
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string? OrganizerId { get; set; } = null;
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
        
        public ICollection<UserInvitation> UserInvitations { get; set; }
    }

    public enum InvitationStatus
    {
        Pending = 1,
        Accepted,
        Rejected
    }
}
