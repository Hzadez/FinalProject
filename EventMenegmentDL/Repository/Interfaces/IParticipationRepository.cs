
using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface IParticipationRepository : IGenericRepository<Participation>
    {
        Task<List<Participation>> GetAllParticipationWithIncludes();
        Task<Participation> GetByIdParticipationWithIncludes(int id);
        Task<List<Participation>> GetParticipationsByUserId(string userId);

        Task<Participation> UpdateAsync(Participation product);





    }

}

