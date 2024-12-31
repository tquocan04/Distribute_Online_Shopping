using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
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

        public Address() { }
    }
    public class CustomerAddress : Address
    {
        public CustomerAddress(Guid customerId) : base()
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            IsDefault = true;
        }
    }

    public class BranchAddress : Address 
    {
        public BranchAddress(Guid branchId) : base()
        {
            Id = Guid.NewGuid();
            BranchId = branchId;
            IsDefault = true;
        }
    }

    public class EmployeeAddress : Address
    {
        public EmployeeAddress(Guid employeeId) : base()
        {
            Id = Guid.NewGuid();
            EmployeeId = employeeId;
            IsDefault = true;
        }
    }

}
