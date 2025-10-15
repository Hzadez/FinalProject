using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface IFeedbackService : IGenericService<FeedbackViewModel, Feedback>
    {
        public Task<List<FeedbackViewModel>> GetAllFeedbackWithIncludes();
        public Task<FeedbackViewModel> GetByIdWithIncludesAsync(int id);
        public Task<FeedbackViewModel> UpdateAsync(FeedbackViewModel entity);
    }
}
