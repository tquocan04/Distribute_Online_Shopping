namespace DTOs.MongoDb
{
    public class RecommendProduct
    {
        public string CategoryId { get; set; }
        public string ProductId { get; set; }
        public long? Action { get; set; } = 0;
        public long? Purchase { get; set; } = 0;
    }
}
