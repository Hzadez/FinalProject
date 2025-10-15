
using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);

        Task<T> DeleteAsync(int id);
    }

}
