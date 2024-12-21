using System.Threading.Tasks;
using DiscountSystem.Application.Services;
using DiscountSystem.Domain.Enums;
using DiscountSystem.API;
using Grpc.Core;

namespace DiscountSystem.API.Services
{
    public class DiscountServiceImpl : DiscountService.DiscountServiceBase
    {
        private readonly DiscountServiceApp _service;

        public DiscountServiceImpl(DiscountServiceApp service)
        {
            _service = service;
        }

        public override async Task<GenerateResponse> GenerateCodes(GenerateRequest request, ServerCallContext context)
        {
            bool result = await _service.GenerateDiscountCodesAsync((int)request.Count, (int)request.Length);
            return new GenerateResponse { Result = result };
        }

        public override async Task<UseCodeResponse> UseCode(UseCodeRequest request, ServerCallContext context)
        {
            DiscountErrorCode result = await _service.UseDiscountCodeAsync(request.Code);
            return new UseCodeResponse { Result = (uint)result };
        }
    }
}