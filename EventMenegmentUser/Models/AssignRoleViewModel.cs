

namespace EventMenegmentAdmin.Models
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public List<RoleViewModel> Roles { get; set; }  = new List<RoleViewModel>();
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
