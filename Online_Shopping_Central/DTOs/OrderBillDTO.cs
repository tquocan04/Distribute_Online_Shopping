namespace Online_Shopping_Central.DTOs
{
    public class OrderBillDTO
    {
        public Guid Id { get; set; }
        public ICollection<ItemDTO>? Items { get; set; }
        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ShippingMethodId { get; set; }
        public string? Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string? PaymentId { get; set; }
        public Guid? VoucherId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
