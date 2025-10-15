using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Implementation;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EventMenegmentSL.Services.Implementation
{
    public class UserInvitationService : IUserInivitationService
    {
        private readonly IUserInivitationRepository _userInvitationRepo;
       
       private readonly IMapper _mapper;

        public UserInvitationService(IUserInivitationRepository userInvitationRepo, IMapper mapper)
        {
            _userInvitationRepo = userInvitationRepo;
            _mapper = mapper;
        }

        public async Task<List<UserInvitationViewModel>> GetByIdUserInivitation(string userId)
        {
            var data = await _userInvitationRepo.GetByUserIdAsync(userId);
            if (data == null)
            {
                return null;
            }
            var invitations =data.Where(ui=>ui.IsAccepted==InvitationStatus.Pending).Select(ui=> new UserInvitationViewModel
            {
                Id = ui.Id,
                InvitationId = ui.InvitationId,
                UserId = ui.UserId,
                Title = ui.Invitation.Title,
                EventDate = ui.Invitation.EventDate,
                EventName = ui.Invitation.Event.Name,
                 Location= ui.Invitation.Event.Location,
                Description = ui.Invitation.Description,

                SentAt = ui.SentAt,

            }).ToList();
            return invitations;



        }

        public async Task<List<UserInvitationViewModel>> GetByInivitationForParticipation(string UserId)
        {

            var data = await _userInvitationRepo.GetByUserIdAsync(UserId);
            if (data == null)
            {
                return null;
            }
            var invitations = data.Where(ui => ui.IsAccepted == InvitationStatus.Accepted).Select(ui => new UserInvitationViewModel
            {
                Id = ui.Id,
                InvitationId = ui.InvitationId,
                User=ui.User,
                UserId = ui.UserId,
                Title = ui.Invitation.Title,
                EventDate = ui.Invitation.Event.Date,
                EventName = ui.Invitation.Event.Name,
                Location = ui.Invitation.Event.Location,
                Description = ui.Invitation.Description,
               IsAccepted = ui.IsAccepted,
               Capacity=ui.Invitation.Event.Location.Capacity,
                SentAt = ui.SentAt,

            }).ToList();
            return invitations;

        }

        public async Task<UserInvitationViewModel> GetByInvitationIdAsync(int invitationId)
        {
           var data = await _userInvitationRepo.GetByInvitationIdAsync(invitationId);
            if (data == null)
            {
                return null;
            }
            var invitation =  new UserInvitationViewModel
            {
                Id = data.Id,
                InvitationId = data.InvitationId,
                UserId = data.UserId,
                Title = data.Invitation.Title,
                EventDate = data.Invitation.EventDate,
                EventName = data.Invitation.Event.Name,
                Location = data.Invitation.Event.Location,
                Description = data.Invitation.Description,

                SentAt = data.SentAt,
            };
            return invitation;
        }

        public async Task<UserInvitationViewModel> UpdateUserAcceptInivitation(UserInvitationViewModel model)
        {
            var entityToUpdate = _mapper.Map<UserInvitation>(model);
            var result = await _userInvitationRepo.UpdateUserAcceptInivitation(entityToUpdate);
            if (result == null)
            {
                return null;
            }
            var updatedResult = _mapper.Map<UserInvitationViewModel>(result);
            return updatedResult;
        }

        public async Task<UserInvitationViewModel> UpdateUserDeclineInivitation(UserInvitationViewModel userInvitation)
        {
            var entityToUpdate = _mapper.Map<UserInvitation>(userInvitation);
            var result = await _userInvitationRepo.UpdateUserDeclineInivitation(entityToUpdate);
            if (result == null)
            {
                return null;
            }
            var updatedResult = _mapper.Map<UserInvitationViewModel>(result);
            return updatedResult;
        }
    }
    }
 
