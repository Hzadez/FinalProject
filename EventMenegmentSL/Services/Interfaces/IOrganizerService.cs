using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface IOrganizerService : IGenericService<OrganizerViewModel, Organizer>
    {
        public Task<List<OrganizerViewModel>> GetAllProductWithIncludes();
        public Task<OrganizerViewModel> GetByIdProductWithIncludes(int id);
        public Task<OrganizerViewModel> UpdateAsync(OrganizerViewModel product);

    }
}
