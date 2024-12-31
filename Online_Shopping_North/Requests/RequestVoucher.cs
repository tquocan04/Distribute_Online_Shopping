namespace Online_Shopping_North.Requests
{
    public class RequestVoucher
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? Percentage { get; set; }
        public decimal? MaxDiscount { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public decimal? MinOrderValue { get; set; }
        public int? Quantity { get; set; }
    }
}
