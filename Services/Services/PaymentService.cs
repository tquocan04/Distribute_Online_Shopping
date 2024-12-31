using AutoMapper;
using DTOs.DTOs;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _paymentRepo;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepo paymentRepo, IMapper mapper) 
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PaymentDTO>> GetPaymentListAsync()
        {
            var payments = await _paymentRepo.GetAllPaymentsAsync();
            return _mapper.Map<IEnumerable<PaymentDTO>>(payments);
        }
    }
}
