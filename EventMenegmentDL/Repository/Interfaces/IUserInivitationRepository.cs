    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface IUserInivitationRepository : IGenericRepository<UserInvitation>
    {
        Task AddAsync(UserInvitation invitations);
        Task<List<UserInvitation>> GetByUserIdAsync(string userId);
        Task<UserInvitation> GetByInvitationIdAsync(int invitationId);
        Task<UserInvitation> UpdateUserAcceptInivitation(UserInvitation userInvitation);
        Task<UserInvitation> UpdateUserDeclineInivitation(UserInvitation userInvitation);
    }
}
