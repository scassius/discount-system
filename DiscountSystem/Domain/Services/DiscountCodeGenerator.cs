using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountSystem.Domain.Constants;
using DiscountSystem.Domain.Entities;
using DiscountSystem.Domain.Enums;
using DiscountSystem.Domain.Errors;
using DiscountSystem.Domain.Interfaces;

namespace DiscountSystem.Domain.Services
{
    public class DiscountCodeGenerator
    {
        private readonly IDiscountCodeRepository _repository;
        private static readonly Random _random = new Random();

        public DiscountCodeGenerator(IDiscountCodeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DiscountCode>> GenerateCodesAsync(int count, int length)
        {
            ValidateParameters(count, length);

            var newCodes = new List<DiscountCode>(count);

            for (int i = 0; i < count; i++)
            {
                string code;
                do
                {
                    code = GenerateRandomCode(length);
                }
                while (await _repository.ExistsAsync(code));

                newCodes.Add(new DiscountCode(code));
            }

            return newCodes;
        }

        private void ValidateParameters(int count, int length)
        {
            if (count <= 0 || count > DiscountConstants.MaxGenerateCount)
                throw new ArgumentOutOfRangeException(nameof(count), ErrorMessages.InvalidCount);

            if (length < DiscountConstants.MinCodeLength || length > DiscountConstants.MaxCodeLength)
                throw new ArgumentOutOfRangeException(nameof(length), ErrorMessages.InvalidLength);
        }

        private string GenerateRandomCode(int length)
        {
            var code = new char[length];
            for (int i = 0; i < length; i++)
            {
                code[i] = DiscountConstants.CodeCharacters[_random.Next(DiscountConstants.CodeCharacters.Length)];
            }
            return new string(code);
        }
    }
}
