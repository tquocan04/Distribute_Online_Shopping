namespace Service.Contracts.Interfaces
{
    public interface IAddressService<T> where T : class
    {
        Task<T> SetAddress(T obj, Guid id);
        Task<string> GetRegionIdOfObject(Guid id);
    }
}
