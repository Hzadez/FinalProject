
using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {

        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Event>> GetAllEventWithIncludes()
        {
            var data = await _context.Events.Include(e => e.Location).Include(e => e.EventType).Include(e => e.Organizer).Where(e=>e.IsDeleted==false).ToListAsync();
            return data;
        }

        public async Task<Event> GetEventWithIncludes(int id)
        {
            var data = await _context.Events.Include(e => e.Location).Include(e => e.EventType).Include(e => e.Organizer).Where(e => e.IsDeleted == false).FirstOrDefaultAsync(e => e.Id == id);
            return data;
        }

        public async Task<Event> UpdateAsync(Event entity)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == entity.Id);
            if (existingEvent != null)
            {
                _context.Entry(existingEvent).State = EntityState.Detached;
            }

            if (existingEvent == null)
            {

                return null;
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
    }
}
