using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_South.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; } = null;
        public Customer? Customer { get; set; }
        [ForeignKey(nameof(Branch))]
        public Guid? BranchId { get; set; } = null;
        public Branch? Branch { get; set; }
        [ForeignKey(nameof(Employee))]
        public Guid? EmployeeId { get; set; } = null;
        public Employee? Employee { get; set; }
        [ForeignKey(nameof(District))]
        public Guid DistrictId { get; set; }
        public District? District { get; set; }
        public string Street { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
