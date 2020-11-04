using POS.Domain.ProductAggregate;
using POS.Domain.Specifications.Dicounts;
using System;
using Xunit;

namespace POS.UnitTests.Specification.Discounts
{
    public class IsQuantityValidSpecTests
    {
        [Fact]
        public void GivenDiscount_WhenTheQuantityIsValid_IsSuccessful()
        {
            // Arrange
            var discount = new Discount(Guid.NewGuid(), 1, 10);
            var spec = new IsQuantityValidSpec();

            // Act
            var result = spec.IsSatisfiedBy(discount);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenDiscount_WhenTheQuantityIsNegative_IsFailed()
        {
            // Arrange
            var discount = new Discount(Guid.NewGuid(), -1, 10);
            var spec = new IsQuantityValidSpec();

            // Act
            var result = spec.IsSatisfiedBy(discount);

            // Assert
            Assert.False(result);
        }
    }
}
