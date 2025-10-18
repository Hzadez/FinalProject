
using Microsoft.AspNetCore.Identity;

namespace EventMenegmentDL.Entity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public ICollection<UserInvitation> UserInvitations { get; set; }
    }
}
