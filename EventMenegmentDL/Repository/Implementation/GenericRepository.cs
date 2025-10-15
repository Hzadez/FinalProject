using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class GenericRepository<T> : Interfaces.IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;


        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new Exception("Entity not found");



            entity.IsDeleted = true;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.AsNoTracking().Where(e => !e.IsDeleted).ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.AsNoTracking().Where(e => !e.IsDeleted).FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            return entity;
        }




    }
}
