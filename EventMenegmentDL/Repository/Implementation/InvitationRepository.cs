using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class InvitationRepository : GenericRepository<Invitation>, IInvitationRepository
    {

        private readonly AppDbContext _context;
        public InvitationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Invitation>> GetAllAsync()
        {
            return await _context.Invitations.Include(i => i.Event).ThenInclude(e => e.Location)
                .Include(i => i.UserInvitations).ThenInclude(s=>s.User)
                .ToListAsync();
        }
        public async Task<Invitation> CreateAsync(Invitation invitation)
        {
            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();
            return invitation;
        }

    }
}
