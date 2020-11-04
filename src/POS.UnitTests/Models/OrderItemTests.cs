using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;
using System;
using Xunit;

namespace POS.UnitTests.Models
{
    public class OrderItemTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(10000)]
        public void GivenOrderItem_WhenTheQuantityIsSetting_IsSuccessful(int quantity)
        {
            // Arrange
            var orderItem = new OrderItem(Guid.NewGuid());

            // Act
            orderItem.SetQuantity(quantity);

            // Assert
            Assert.Equal(quantity, orderItem.Quantity);
        }

        [Theory]
        [InlineData(2,1)]
        [InlineData(2,100)]
        [InlineData(2,10000)]
        public void GivenOrderItem_WhenTheQuantityIsAdding_IsSuccessful(int initQuantity, int quantityToAdd)
        {
            // Arrange
            var orderItem = new OrderItem(Guid.NewGuid(), initQuantity);

            // Act
            orderItem.AddQuantity(quantityToAdd);

            // Assert
            Assert.Equal(initQuantity + quantityToAdd, orderItem.Quantity);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-10000)]
        public void GivenOrderItem_WhenTheNegativeQuantityIsAdding_IsFailed(int quantityToAdd)
        {
            // Arrange
            var orderItem = new OrderItem(Guid.NewGuid());
            var notification = new Notification();

            // Act
            orderItem.AddQuantity(quantityToAdd);
            orderItem.Validate(notification);

            // Assert
            Assert.False(orderItem.IsValid);
            Assert.True(notification.HasErrors);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-10000)]
        public void GivenOrderItem_WhenTheNegativeQuantityIsSetting_IsFailed(int quantity)
        {
            // Arrange
            var orderItem = new OrderItem(Guid.NewGuid());
            var notification = new Notification();

            // Act
            orderItem.SetQuantity(quantity);
            orderItem.Validate(notification);

            // Assert
            Assert.False(orderItem.IsValid);
            Assert.True(notification.HasErrors);
        }

    }
}
