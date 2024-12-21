using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountSystem.Domain.Entities;
using DiscountSystem.Domain.Interfaces;
using DiscountSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscountSystem.Infrastructure.Repositories
{
    public class DiscountCodeRepository : IDiscountCodeRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscountCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string code)
        {
            return await _context.DiscountCodes.AnyAsync(dc => dc.Code == code);
        }

        public async Task AddRangeAsync(IEnumerable<DiscountCode> codes)
        {
            await _context.DiscountCodes.AddRangeAsync(codes);
        }

        public async Task<DiscountCode> GetByCodeAsync(string code)
        {
            return await _context.DiscountCodes.FirstOrDefaultAsync(dc => dc.Code == code);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAllCodesAsync()
        {
            return await _context.DiscountCodes.Select(dc => dc.Code).ToListAsync();
        }
    }
}