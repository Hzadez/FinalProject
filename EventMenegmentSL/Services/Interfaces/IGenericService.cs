

using EventMenegmentDL.Entity;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface IGenericService<TVM, TEntity> where TEntity : BaseEntity, new()
    {
        Task<TVM> GetByIdAsync(int id);
        Task<IEnumerable<TVM>> GetAllAsync();
        Task<TVM> AddAsync(TVM entity);

        Task<bool> DeleteAsync(int id);

    }

}
