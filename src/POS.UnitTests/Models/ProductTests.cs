using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;
using System;
using Xunit;

namespace POS.UnitTests.Models
{
    public class ProductTests
    {
        [Theory]
        [InlineData(1.1)]
        [InlineData(1.55)]
        [InlineData(0.25)]
        public void GivenProduct_WhenThePriceIsSetting_IsSuccessful(decimal price)
        {
            // Arrange
            var product = new Product("dammy name", 1);

            // Act
            product.SetPrice(price);

            // Assert
            Assert.Equal(price, product.Price);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-10000)]
        public void GivenProduct_WhenTheNegativePriceIsSetting_IsFailed(int price)
        {
            // Arrange
            var product = new Product("dammy name", 1);
            var notification = new Notification();

            // Act
            product.SetPrice(price);
            product.Validate(notification);

            // Assert
            Assert.False(product.IsValid);
            Assert.True(notification.HasErrors);
        }

        [Theory]
        [InlineData("dammy A")]
        [InlineData("dammy B")]
        [InlineData("dammy C")]
        public void GivenProduct_WhenTheNameIsSetting_IsSuccessful(string name)
        {
            // Arrange
            var product = new Product("dammy name", 1);

            // Act
            product.SetName(name);

            // Assert
            Assert.Equal(name, product.Name);
        }

        [Fact]
        public void GivenProduct_WhenTheNotValidNameIsSetting_IsFailed()
        {
            // Arrange
            var product = new Product("dammy name", 1);
            var notification = new Notification();

            // Act
            product.SetName(null);
            product.Validate(notification);

            // Assert
            Assert.False(product.IsValid);
            Assert.True(notification.HasErrors);
        }

        [Fact]
        public void GivenProduct_WhenTheProductIsDeleting_IsSuccessful()
        {
            // Arrange
            var product = new Product("dammy name", 1);

            // Act
            product.MarkAsDeleted();

            // Assert
            Assert.True(product.IsDeleted);
        }

        [Fact]
        public void GivenProduct_WhenTheDiscountIsSetting_IsSuccessful()
        {
            // Arrange
            var product = new Product("dammy name", 1);

            // Act
            product.SetDiscount(new Discount(product.Id));

            // Assert
            Assert.True(product.Discount != null);
        }

        [Fact]
        public void GivenProduct_WhenTheInvalidDiscountIsSetting_IsFailed()
        {
            // Arrange
            var product = new Product("dammy name", 1);
            var notification = new Notification();

            // Act
            product.SetDiscount(new Discount(Guid.Empty));
            product.Validate(notification);

            // Assert
            Assert.False(product.IsValid);
            Assert.True(notification.HasErrors);
        }
    }
}
