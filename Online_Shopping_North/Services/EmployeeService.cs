using AutoMapper;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly IAddressService<EmployeeDTO> _addressService;

        public EmployeeService(IEmployeeRepo employeeRepo, IMapper mapper,
            IAddressRepo addressRepo,
            IBranchRepo branchRepo,
            IAddressService<EmployeeDTO> addressService)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _branchRepo = branchRepo;
            _addressService = addressService;
        }

        public async Task AddNewEmployee(Guid id, RequestEmployee employee)
        {
            Employee emp = new()
            {
                Id = id,
                Dob = new DateOnly(employee.Year, employee.Month, employee.Day)
            };
            _mapper.Map(employee, emp);

            await _employeeRepo.AddNewStaff(emp);


            Address address = new()
            {
                Id = Guid.NewGuid(),
                EmployeeId = emp.Id,
                IsDefault = true
            };
            _mapper.Map(employee, address);

            await _addressRepo.CreateNewAddress(address);
            var empDTO = _mapper.Map<EmployeeDTO>(emp);
            await _addressService.SetAddress(empDTO, empDTO.Id);

        }

        public async Task DeleteEmployee(string id)
        {
            var emp = await _employeeRepo.GetStaffAsync(Guid.Parse(id));

            await _employeeRepo.DeleteStaffAsync(emp);
        }

        public async Task<EmployeeDTO> GetProfileEmployee(string id)
        {
            var emp = await _employeeRepo.GetStaffAsync(Guid.Parse(id));
            var empDTO = _mapper.Map<EmployeeDTO>(emp);
            empDTO.Year = emp.Dob.Year;
            empDTO.Month = emp.Dob.Month;
            empDTO.Day = emp.Dob.Day;

            empDTO = await _addressService.SetAddress(empDTO, empDTO.Id);

            var branch = await _branchRepo.GetBranchAsync(empDTO.BranchId);
            if (branch != null)
            {
                empDTO.BranchName = branch.Name;
            }

            return empDTO;
        }

        public async Task UpdateProfile(string id, RequestEmployee requestEmployee)
        {
            var emp = await _employeeRepo.GetStaffAsync(Guid.Parse(id));
            
            var existingAddress = await _addressRepo.GetAddressByObjectIdAsync(Guid.Parse(id));

            if (existingAddress != null)
            {
                if (existingAddress.DistrictId != requestEmployee.DistrictId || existingAddress.Street != requestEmployee.Street)
                {
                    _mapper.Map(requestEmployee, existingAddress);
                    await _addressRepo.UpdateAddress(existingAddress);
                }
            }

            DateOnly dob = new(requestEmployee.Year, requestEmployee.Month, requestEmployee.Day);
            
            _mapper.Map(requestEmployee, emp);

            emp.Dob = dob;
            await _employeeRepo.UpdateProfileStaff(emp);

        }
    }
}
