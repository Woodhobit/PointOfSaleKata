using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;
using System;
using Xunit;

namespace POS.UnitTests.Models
{
    public class DiscountTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(10000)]
        public void GivenDiscount_WhenTheQuantityIsSetting_IsSuccessful(int quantity)
        {
            // Arrange
            var discount = new Discount(Guid.NewGuid());

            // Act
            discount.SetQuantity(quantity);

            // Assert
            Assert.Equal(quantity, discount.Quantity);
        }

        [Theory]
        [InlineData(1.1)]
        [InlineData(1.55)]
        [InlineData(0.25)]
        public void GivenDiscount_WhenThePriceIsSetting_IsSuccessful(decimal price)
        {
            // Arrange
            var discount = new Discount(Guid.NewGuid());

            // Act
            discount.SetDiscountPrice(price);

            // Assert
            Assert.Equal(price, discount.DiscountPrice);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-10000)]
        public void GivenDicount_WhenTheNegativeQuantityIsSetting_IsFailed(int quantity)
        {
            // Arrange
            var discount = new Discount(Guid.NewGuid());
            var notification = new Notification();

            // Act
            discount.SetQuantity(quantity);
            discount.Validate(notification);

            // Assert
            Assert.False(discount.IsValid);
            Assert.True(notification.HasErrors);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-10000)]
        public void GivenDicount_WhenTheNegativePriceIsSetting_IsFailed(int price)
        {
            // Arrange
            var discount = new Discount(Guid.NewGuid());
            var notification = new Notification();

            // Act
            discount.SetDiscountPrice(price);
            discount.Validate(notification);

            // Assert
            Assert.False(discount.IsValid);
            Assert.True(notification.HasErrors);
        }

        [Fact]
        public void GivenDiscount_WhenTheProductIdIsNotAdded_IsFailed()
        {
            // Arrange
            var discount = new Discount(Guid.NewGuid());
            var notification = new Notification();

            // Act
            discount.Validate(notification);

            // Assert
            Assert.False(discount.IsValid);
            Assert.True(notification.HasErrors);
        }
    }
}
