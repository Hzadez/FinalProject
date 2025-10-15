using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface IOrganizerRepository : IGenericRepository<Organizer>
    {
        Task<List<Organizer>> GetAllOrganizerWithIncludes();
        Task<Organizer> GetByIdOrganizerWithIncludes(int id);

        Task<Organizer> Update(Organizer product);





    }

}
