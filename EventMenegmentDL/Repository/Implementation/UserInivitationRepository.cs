using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class UserInivitationRepository : GenericRepository<UserInvitation>, IUserInivitationRepository
    {
        private readonly AppDbContext _context;

        public UserInivitationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(UserInvitation invitations)
        {
            await _context.UserInvitations.AddAsync(invitations);
            await _context.SaveChangesAsync();
        }


        public Task<UserInvitation> GetByInvitationIdAsync(int invitationId)
        {
            return _context.UserInvitations
                .Where(ui => ui.Id == invitationId && ui.IsAccepted == InvitationStatus.Pending)
                .Include(ui => ui.Invitation)
                    .ThenInclude(e => e.Event)
                        .ThenInclude(l => l.Location)
                .FirstOrDefaultAsync();
        }



        public async Task<List<UserInvitation>> GetByUserIdAsync(string userId)
        {
            var data = await _context.UserInvitations
            .Where(x => x.UserId == userId )
             .Include(i => i.User)        
              .Include(i => i.Invitation)
               .ThenInclude(inv => inv.Event)
                 .ThenInclude(evt => evt.Location)
                     .ToListAsync();
             return data;

        }





        public async Task<UserInvitation> UpdateUserAcceptInivitation(UserInvitation userInvitation)
        {
            var data = await _context.UserInvitations.FirstOrDefaultAsync(ui => ui.Id == userInvitation.Id);
            if (data == null)
            {
                return null;
            }
            data.IsAccepted = InvitationStatus.Accepted; ;

            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<UserInvitation> UpdateUserDeclineInivitation(UserInvitation userInvitation)
        {

            var data = await _context.UserInvitations.FirstOrDefaultAsync(ui => ui.Id == userInvitation.Id);
            if (data == null)
            {
                return null;
            }
            data.IsAccepted = InvitationStatus.Rejected;

            await _context.SaveChangesAsync();
            return data;

        }
    }
}
