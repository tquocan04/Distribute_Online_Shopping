namespace Service.Contracts.Interfaces
{
    public interface IStatisticService
    {
        Task<Dictionary<string, decimal?>> GetRevenueCategoriesInYearMonth(int year, int month);
        Task<Dictionary<string, decimal?>> GetRevenueCategoriesInYear(int year);
        Task<Dictionary<string, decimal?>> GetRevenueByCategoriesInYearMonth(Guid categoryId, int year, int month);
        Task<Dictionary<string, decimal?>> GetRevenueByCategoriesInYear(Guid categoryId, int year);
    }
}
