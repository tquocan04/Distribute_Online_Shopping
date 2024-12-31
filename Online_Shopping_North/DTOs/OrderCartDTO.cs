namespace Online_Shopping_North.DTOs
{
    public class OrderCartDTO
    {
        //public Guid Id { get; set; }
        public ICollection<ItemDTO>? Items { get; set; }
        public decimal TotalPrice { get; set; } = 0;
    }
}
