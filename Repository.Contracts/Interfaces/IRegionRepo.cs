using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IRegionRepo
    {
        Task<IEnumerable<Region>> GetAllRegions();
    }
}
