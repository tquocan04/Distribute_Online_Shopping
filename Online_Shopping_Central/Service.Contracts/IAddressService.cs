namespace Online_Shopping_Central.Service.Contracts
{
    public interface IAddressService<T> where T : class
    {
        Task<T> SetAddress(T obj, Guid id);
    }
}
