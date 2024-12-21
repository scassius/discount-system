using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using DiscountSystem.Domain.Entities;
using DiscountSystem.Domain.Interfaces;
using DiscountSystem.Domain.Services;
using DiscountSystem.Application.Services;

namespace DiscountSystem.Tests
{
    public class GenerateCodesTests
    {
        [Fact]
        public async Task GenerateDiscountCodesAsync_ValidParameters_AddsCodes()
        {
            int count = 5;
            int length = 8;
            var mockRepo = new Mock<IDiscountCodeRepository>();
            var generator = new DiscountCodeGenerator(mockRepo.Object);
            var discountService = new DiscountServiceApp(generator, mockRepo.Object);

            var result = await discountService.GenerateDiscountCodesAsync(count, length);

            mockRepo.Verify(repo => repo.AddRangeAsync(It.Is<IEnumerable<DiscountCode>>(codes => codes != null && codes.Count() == count)), Times.Once);
            mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [InlineData(0, 8)]
        [InlineData(2001, 8)]
        [InlineData(10, 6)]
        [InlineData(10, 9)]
        public async Task GenerateDiscountCodesAsync_InvalidParameters_ThrowsArgumentOutOfRangeException(int count, int length)
        {
            var mockRepo = new Mock<IDiscountCodeRepository>();
            var generator = new DiscountCodeGenerator(mockRepo.Object);
            var discountService = new DiscountServiceApp(generator, mockRepo.Object);

            var result = await discountService.GenerateDiscountCodesAsync(count, length);

            Assert.False(result);

            mockRepo.Verify(repo => repo.AddRangeAsync(It.IsAny<IEnumerable<DiscountCode>>()), Times.Never);
            mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }
    }
}
