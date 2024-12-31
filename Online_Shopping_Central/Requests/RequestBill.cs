namespace Online_Shopping_Central.Requests
{
    public class RequestBill
    {
        public string? PaymentId { get; set; }
        public string? ShippingMethodId { get; set; }
        public string? VoucherCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string RegionId { get; set; } = null!;
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; } = null!;
        public string? Note { get; set; }
    }
}
