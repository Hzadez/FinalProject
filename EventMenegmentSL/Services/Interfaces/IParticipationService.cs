using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface IParticipationService : IGenericService<ParticipationViewModel, Participation>
    {

        public Task<List<ParticipationViewModel>> GetAllProductWithIncludes();
        public Task<ParticipationViewModel> GetByIdProductWithIncludes(int id);
        public Task<List<ParticipationViewModel>> GetParticipationsByUserId(string userId);
        public Task<ParticipationViewModel> UpdateAsync(ParticipationViewModel product);


    }
}
