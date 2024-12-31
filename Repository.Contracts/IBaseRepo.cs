namespace Repository.Contracts
{
    public interface IBaseRepo<T>
    {
        Task<T?> GetByIdAsync(Guid id);
    }
}
