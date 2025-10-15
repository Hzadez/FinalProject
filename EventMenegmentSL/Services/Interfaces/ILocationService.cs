using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface ILocationService : IGenericService<LocationViewModel, Location>
    {
        Task<LocationViewModel> GetByIdWithIncludesAsync(int id);
        Task<LocationViewModel> UpdateAsync(LocationViewModel entity);

    }

}

