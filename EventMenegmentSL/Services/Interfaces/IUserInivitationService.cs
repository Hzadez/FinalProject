using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface IUserInivitationService
    {
       Task<List<UserInvitationViewModel>> GetByIdUserInivitation(string UserId);
       Task<List<UserInvitationViewModel>> GetByInivitationForParticipation(string UserId);
        Task <UserInvitationViewModel>GetByInvitationIdAsync(int invitationId);
        Task<UserInvitationViewModel> UpdateUserAcceptInivitation(UserInvitationViewModel userInvitation);
        Task<UserInvitationViewModel> UpdateUserDeclineInivitation(UserInvitationViewModel userInvitation);

    }
}
