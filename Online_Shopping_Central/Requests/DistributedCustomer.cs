namespace Online_Shopping_Central.Requests
{
    public class DistributedCustomer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string RegionId { get; set; }
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; }
        public string? Picture { get; set; }
        public Guid OrderId { get; set; }
    }
}
