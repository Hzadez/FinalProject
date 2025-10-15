using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface IInvitationRepository : IGenericRepository<Invitation>
    {
        Task<List<Invitation>> GetAllAsync();
        Task<Invitation> CreateAsync(Invitation invitation);



    }

}
