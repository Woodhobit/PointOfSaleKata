using POS.Application.Exception;
using POS.FunctionalTests.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace POS.FunctionalTests.ProcessOrderUseCase
{
    public class ProcessOrderTests : BaseTests
    {
        public ProcessOrderTests(DataFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData("ABCDABA", 13.25)]
        [InlineData("CCCCCCC", 6.0)]
        [InlineData("ABCD", 7.25)]
        public async Task ProcessOrder_WhenAtLeastOneProductIsAdded_IsSuccessful(string products,decimal totalPrice)
        {
            // Arrange
            var terminal = this.PointOfSaleTerminal;
            await terminal.InitNewOrderAsync(Guid.NewGuid());

            // Act
            foreach (char product in products)
            {
                await terminal.ScanAsync(product.ToString());
            }

            var total = await terminal.CalculateTotalAsync();

            //Assert
            Assert.Equal(totalPrice, total);
        }

        [Fact]
        public async Task ProcessOrder_WhenNoProductIsAdded_ZeroShouldBeReturned()
        {
            // Arrange
            var terminal = this.PointOfSaleTerminal;
            await terminal.InitNewOrderAsync(Guid.NewGuid());

            // Act
            var total = await terminal.CalculateTotalAsync();

            Assert.Equal(0, total);
        }

        [Theory]
        [InlineData("E")]
        [InlineData("Abc")]
        [InlineData("3M")]
        public async Task ProcessOrder_WhenInvalidProductIsAdded__IsFailed(string product)
        {
            // Arrange
            var terminal = this.PointOfSaleTerminal;
            await terminal.InitNewOrderAsync(Guid.NewGuid());

            // Act
            Func<Task> act = () => terminal.ScanAsync(product);

            //Assert
            var exception = await Assert.ThrowsAsync<POSException>(act);
        }
    }
}
