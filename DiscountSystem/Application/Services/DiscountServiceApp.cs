using System;
using System.Threading.Tasks;
using DiscountSystem.Domain.Enums;
using DiscountSystem.Domain.Errors;
using DiscountSystem.Domain.Interfaces;
using DiscountSystem.Domain.Services;

namespace DiscountSystem.Application.Services
{
    public class DiscountServiceApp
    {
        private readonly DiscountCodeGenerator _generator;
        private readonly IDiscountCodeRepository _repository;

        public DiscountServiceApp(DiscountCodeGenerator generator, IDiscountCodeRepository repository)
        {
            _generator = generator;
            _repository = repository;
        }

        public async Task<bool> GenerateDiscountCodesAsync(int count, int length)
        {
            try
            {
                var newCodes = await _generator.GenerateCodesAsync(count, length);
                await _repository.AddRangeAsync(newCodes);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<DiscountErrorCode> UseDiscountCodeAsync(string code)
        {
            var discountCode = await _repository.GetByCodeAsync(code);
            if (discountCode == null)
                return DiscountErrorCode.CodeNotFound;

            if (discountCode.IsUsed)
                return DiscountErrorCode.CodeAlreadyUsed;

            try
            {
                discountCode.Use();
                await _repository.SaveChangesAsync();
                return DiscountErrorCode.Success;
            }
            catch
            {
                return DiscountErrorCode.GenerationFailed;
            }
        }
    }
}