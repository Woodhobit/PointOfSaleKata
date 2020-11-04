using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;
using System;
using Xunit;

namespace POS.UnitTests.Models
{
    public class OrderTests
    {
        [Fact]
        public void GivenOrder_WhenTheOrderIsCanceling_IsSuccessful()
        {
            // Arrange
            var order= new Order(Guid.NewGuid());

            // Act
            order.Cancel();

            // Assert
            Assert.True(order.Canceled);
        }

        [Fact]
        public void GivenOrder_WhenTheOrderIsCleaning_IsSuccessful()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());

            // Act
            order.Clean();

            // Assert
            Assert.True(order.Items.Count == 0);
        }

        [Fact]
        public void GivenOrder_WhenTheEmptyItemsAreRemoving_IsSuccessful()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.AddItem(Guid.NewGuid(), 0);

            // Act
            order.RemoveEmptyItems();

            // Assert
            Assert.True(order.Items.Count == 0);
        }

        [Fact]
        public void GivenOrder_WhenTheItemIsAdding_IsSuccessful()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());

            // Act
            order.AddItem(Guid.NewGuid(), 1);

            // Assert
            Assert.True(order.Items.Count == 1);
        }

        [Fact]
        public void GivenOrder_WhenTheCustomerIdIsNotAdded_IsFailed()
        {
            // Arrange
            var order = new Order(Guid.Empty);
            var notification = new Notification();

            // Act
            order.Validate(notification);

            // Assert
            Assert.False(order.IsValid);
            Assert.True(notification.HasErrors);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-10000)]
        public void GivenOrder_WhenTheNegativeQuantityIsAdding_IsFailed(int quantity)
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            var notification = new Notification();

            // Act
            order.AddItem(Guid.NewGuid(), quantity);
            order.Validate(notification);

            // Assert
            Assert.False(order.IsValid);
            Assert.True(notification.HasErrors);
        }
    }
}
