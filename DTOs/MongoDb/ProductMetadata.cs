using Entities.Entities;

namespace DTOs.MongoDb
{
    public class ProductMetadata
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public string? CategoryName { get; set; }
        public long? Action { get; set; } = 0;
        public long? Purchase { get; set; } = 0;
        public long? Total { get; set; } = 0;



        public void IncreaseAction(long amount)
        {
            this.Action += amount;
        }

        public void IncreasePurchase(long amount)
        {
            this.Purchase += amount;
        }
        
        public void IncreaseTotal(long amount)
        {
            this.Total += amount;
        }
    }
}
