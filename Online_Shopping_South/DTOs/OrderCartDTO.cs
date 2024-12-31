namespace Online_Shopping_South.DTOs
{
    public class OrderCartDTO
    {
        public ICollection<ItemDTO>? Items { get; set; }
        public decimal TotalPrice { get; set; } = 0;
    }
}
