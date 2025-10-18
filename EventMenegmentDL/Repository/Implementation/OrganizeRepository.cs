
using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class OrganizeRepository : GenericRepository<Organizer>, IOrganizerRepository
    {
        private readonly AppDbContext _context;
        public OrganizeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Organizer>> GetAllOrganizerWithIncludes()
        {
            var organizers = await _context.Organizers.Include(o => o.Events).Where(o => o.IsDeleted == false).ToListAsync();
            return organizers;
        }

        public async Task<Organizer> GetByIdOrganizerWithIncludes(int id)
        {
            var data = await _context.Organizers.Include(o => o.Events).Where(o => o.IsDeleted == false).FirstOrDefaultAsync(o => o.Id == id);
            return data;
        }

        public async Task<Organizer> Update(Organizer entity)
        {
            // BURADA _context.Update çağırmırıq, çünki entity artıq tracked-dir
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
