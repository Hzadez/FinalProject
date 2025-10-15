
using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface IInvitationService 
    {
        Task CreateInvitationWithUsersAsync(InvitationViewModel model);
        Task<List<Invitation>> GetAllAsyncWithIncludes();
        Task<InvitationViewModel> CreateAsync(InvitationViewModel model); 

    }
}