using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class ParticipationRepository : GenericRepository<Participation>, IParticipationRepository
    {
        private readonly AppDbContext _context;
        public ParticipationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Participation>> GetAllParticipationWithIncludes()
        {
            var data = await _context.Participations.Include(p => p.Invitation)
                                               .ThenInclude(p => p.Event)
                                               .ThenInclude(p => p.Participants)
                                               .ToListAsync();

            return data;
        }

        public async Task<Participation> GetByIdParticipationWithIncludes(int id)
        {
            var data = await _context.Participations.Include(p => p.Invitation)
                                               .ThenInclude(p => p.Event)
                                               .ThenInclude(p => p.Participants)
                                               .FirstOrDefaultAsync(p => p.Id == id);
            return data;
        }

        public Task<List<Participation>> GetParticipationsByUserId(string userId)
        {
            var data= _context.Participations.Where(pr=>pr.UserId==userId).Include(p => p.Invitation)
                                               .ThenInclude(p => p.Event)
                                               .ThenInclude(p => p.Participants)
                                               .ToListAsync();
            return data;
        }

        public async Task<Participation> UpdateAsync(Participation product)
        {
            var existingParticipation = await _context.Participations.FindAsync(product.Id);
            if (existingParticipation != null)
            {

                _context.Update(existingParticipation);
                await _context.SaveChangesAsync();
                return existingParticipation;
            }
            return null;
        }
    }
}
