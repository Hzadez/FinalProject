using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Implementation;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Implementation
{
    public class InvitiationService :  IInvitationService
    {


        private readonly IInvitationRepository _invitationRepo;
        private readonly IUserInivitationRepository _userInvitationRepo;
       

        public InvitiationService(IInvitationRepository invitationRepo, IUserInivitationRepository userInvitationRepo)
        {
            _invitationRepo = invitationRepo;
            _userInvitationRepo = userInvitationRepo;
            
        }

        public async Task<InvitationViewModel> CreateAsync(InvitationViewModel model)
        {
            var inivitation = new Invitation
            {
                Title = model.Title,
                Description = model.Description,
                EventDate = model.EventDate,
                EventId = model.EventId,
                Id = model.Id,
                
                Status = InvitationStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
           await _invitationRepo.AddAsync(inivitation);
            return model;
        }

        public async Task CreateInvitationWithUsersAsync(InvitationViewModel model)
        {
            // 1. Yeni Invitation yarat
            var invitation = new Invitation
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                EventId = model.EventId,
                EventDate = model.EventDate,
                Status = InvitationStatus.Pending,
                CreatedAt = DateTime.UtcNow,



            };

            await _invitationRepo.AddAsync(invitation);


            foreach (var userId in model.SelectedUserIds)
            {

                var userInvitation = new UserInvitation
                {
                    InvitationId = invitation.Id,
                    UserId = userId,
                    SentAt = DateTime.UtcNow,
                    IsAccepted = InvitationStatus.Pending,
                };
                await _userInvitationRepo.AddAsync(userInvitation);
            }



        }
        public async Task<List<Invitation>> GetAllAsyncWithIncludes()
        {
           return await _invitationRepo.GetAllAsync();
        }




    }
}
