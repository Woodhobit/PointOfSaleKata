using POS.Domain.ProductAggregate;
using POS.Domain.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace POS.UnitTests.Specification.Products
{
    public class IsNameMaxLengthValidSpecTests
    {
        [Fact]
        public void GivenProduct_WhenTheNameIsValid_IsSuccessful()
        {
            // Arrange
            var product = new Product("dammy name", 10);
            var spec = new IsNameMaxLengthValidSpec();

            // Act
            var result = spec.IsSatisfiedBy(product);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenProduct_WhenTheNameIsEmpty_IsFailed()
        {
            // Arrange
            var product = new Product(null, 10);
            var spec = new IsNameMaxLengthValidSpec();

            // Act
            var result = spec.IsSatisfiedBy(product);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenProduct_WhenTheNameIsMoreThanLimit_IsFailed()
        {
            // Arrange
            var product = new Product(new string('c',300), 10);
            var spec = new IsNameMaxLengthValidSpec();

            // Act
            var result = spec.IsSatisfiedBy(product);

            // Assert
            Assert.False(result);
        }
    }
}
