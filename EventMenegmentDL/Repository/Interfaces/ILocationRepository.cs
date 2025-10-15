using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location>
    {

        Task<Location> GetByIdWithIncludes(int id);
        Task<Location> UpdateAsync(Location entity);

    }
}
