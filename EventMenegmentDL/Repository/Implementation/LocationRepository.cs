using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly AppDbContext _context;
        public LocationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

       

        public async Task<Location> GetByIdWithIncludes(int Id)
        {
            var data = await _context.Locations.Include(l => l.Events).Where(l => l.IsDeleted == false).FirstOrDefaultAsync();
            return data;
        }

        public async Task<Location> UpdateAsync(Location entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Location entity cannot be null");
            }
            var existingEntity = _context.Locations.FirstOrDefault(x => x.Id == entity.Id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Location with ID {entity.Id} not found;");
            }
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
