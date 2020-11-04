using POS.Domain.ProductAggregate;
using POS.Domain.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace POS.UnitTests.Specification.Products
{
    public class IsPriceValidSpecTests
    {
        [Fact]
        public void GivenProduct_WhenThePriceIsValid_IsSuccessful()
        {
            // Arrange
            var product = new Product("dammy name", 10);
            var spec = new IsPriceValidSpec();

            // Act
            var result = spec.IsSatisfiedBy(product);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenProduct_WhenThePriceIsNegative_IsFailed()
        {
            // Arrange
            var product = new Product("dammy name", -10);
            var spec = new IsPriceValidSpec();

            // Act
            var result = spec.IsSatisfiedBy(product);

            // Assert
            Assert.False(result);
        }
    }
}
