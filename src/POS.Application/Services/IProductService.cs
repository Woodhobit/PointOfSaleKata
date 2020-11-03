using POS.Application.Common;
using POS.Domain.ProductAggregate;
using System;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public interface IProductService
    {
        Task<Result<Product>> CreateOrUpdateDiscountAsync(Guid productId, int quantity, decimal discountPrice);
        Task<Result<Product>> CreateProductAsync(string name, decimal price);
        Task<Result<Product>> GetProductAsync(string name);
        Task<Result<Product>> GetProductAsync(Guid id);
        Task<Result<Product>> UpdateProductAsync(Guid id, string name, decimal price, bool isDeleted = false);
    }
}