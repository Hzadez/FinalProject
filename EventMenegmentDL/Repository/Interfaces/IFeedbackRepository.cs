using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task<List<Feedback>> GetAllFeedbackWithIncludes();
        Task<Feedback> GetByIdFeedbackWithIncludes(int id);
        Task<Feedback> UpdateAsync(Feedback entity);
    }
}
