using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly AppDbContext _context;
        public FeedbackRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Feedback>> GetAllFeedbackWithIncludes()
        {
            var data = await _context.Feedbacks.Include(f => f.Event)
                .Include(f => f.Person)
                .ToListAsync();
            return data;

        }

        public async Task<Feedback> GetByIdFeedbackWithIncludes(int id)
        {
            var data = await _context.Feedbacks.Include(f => f.Event)
                .Include(f => f.Person)
                .FirstOrDefaultAsync(f => f.Id == id);
            return data;
        }

        public async Task<Feedback> UpdateAsync(Feedback entity)
        {
            var data = await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == entity.Id);
            if (data != null)
            {

                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            return data;
        }
    }
}
