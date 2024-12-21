using System.Collections.Generic;
using System.Threading.Tasks;
using DiscountSystem.Domain.Entities;

namespace DiscountSystem.Domain.Interfaces
{
    public interface IDiscountCodeRepository
    {
        Task<bool> ExistsAsync(string code);
        Task AddRangeAsync(IEnumerable<DiscountCode> codes);
        Task<DiscountCode> GetByCodeAsync(string code);
        Task SaveChangesAsync();
        Task<IEnumerable<string>> GetAllCodesAsync();
    }
}