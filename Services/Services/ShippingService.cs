using AutoMapper;
using DTOs.DTOs;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IShippingRepo _shippingRepo;
        private readonly IMapper _mapper;

        public ShippingService(IShippingRepo shippingRepo, IMapper mapper) 
        {
            _shippingRepo = shippingRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShippingDTO>> GetShippingListAsync()
        {
            var list = await _shippingRepo.GetAllShippingMethods();
            return _mapper.Map<IEnumerable<ShippingDTO>>(list);
        }
    }
}
