
using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class EventTypeRepository : GenericRepository<EventType>, IEventTypeRepository
    {
        private readonly AppDbContext _context;
        public EventTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<EventType> UpdateAsync(EventType entity)
        {

            var existingEntity = _context.EventTypes.FirstOrDefault(x => x.Id == entity.Id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            if (existingEntity == null)
            {

                return null;
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
    }
}
