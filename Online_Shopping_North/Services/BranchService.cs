using AutoMapper;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IAddressService<BranchDTO> _addressService;

        public BranchService(IBranchRepo branchRepo, IMapper mapper, IAddressRepo addressRepo,
            IAddressService<BranchDTO> addressService)
        {
            _branchRepo = branchRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _addressService = addressService;
        }

        public async Task<BranchDTO> AddNewBranch(string id, RequestBranch requestBranch)
        {
            Branch branch = new Branch
            {
                Id = Guid.Parse(id),
            };

            _mapper.Map(requestBranch, branch);

            BranchDTO branchDTO = new BranchDTO();
            branchDTO = _mapper.Map(await _branchRepo.AddNewBranchAsync(branch), branchDTO);

            Address address = new Address
            {
                Id = Guid.NewGuid(),
                BranchId = branch.Id,
                IsDefault = true,
            };
            _mapper.Map(requestBranch, address);

            await _addressRepo.CreateNewAddress(address);

            return branchDTO;
        }

        public async Task<BranchDTO> GetBranch(string id)
        {
            var branch = await _branchRepo.GetBranchAsync(Guid.Parse(id));

            var branchDTO = _mapper.Map<BranchDTO>(branch);

            branchDTO = await _addressService.SetAddress(branchDTO, branchDTO.Id);

            return branchDTO;
        }

        public async Task<List<BranchDTO>> GetBranchList()
        {
            var branchList = await _branchRepo.GetBranchListAsync();
            var branchDtoList = _mapper.Map<IEnumerable<BranchDTO>>(branchList);

            var list = branchDtoList.ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                list[i] = await _addressService.SetAddress(list[i], list[i].Id);
            }
            return list;
        }

        public async Task DeleteBranch(string id)
        {
            var branch = await _branchRepo.GetBranchAsync(Guid.Parse(id));

            await _branchRepo.DeleteBranchAsync(branch);
        }

        public async Task UpdateBranch(string id, RequestBranch requestBranch)
        {
            BranchDTO branchDTO = _mapper.Map<BranchDTO>(requestBranch);
            Branch branch = _mapper.Map<Branch>(branchDTO);
            branch.Id = Guid.Parse(id);

            var existingAddress = await _addressRepo.GetAddressByObjectIdAsync(branch.Id);

            if (existingAddress != null)
            {
                if (existingAddress.DistrictId != requestBranch.DistrictId || existingAddress.Street != requestBranch.Street)
                {
                    _mapper.Map(requestBranch, existingAddress);
                    await _addressRepo.UpdateAddress(existingAddress);
                }
            }

            await _branchRepo.UpdateBranchAsync(branch);
        }
    }
}
