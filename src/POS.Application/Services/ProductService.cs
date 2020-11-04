using POS.Application.Common;
using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository<Guid> productRepository;

        public ProductService(IProductRepository<Guid> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Result<Product>> CreateProductAsync(string name, decimal price)
        {
            var existed = await this.productRepository.GetByNameAsync(name);
            if (existed != null)
            {
                return new Result<Product>($"Product {name} is already added");
            }

            var product = new Product(name, price);

            var validationResult = this.ValidateProduct(product);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();

            return new Result<Product>(product);
        }

        public async Task<Result<Product>> UpdateProductAsync(Guid id, string name, decimal price, bool isDeleted = false)
        {
            var result = await this.GetProductAsync(id);
            if (result.IsSuccess == false)
            {
                return result;
            }

            var product = result.Value;
            product.SetName(name);
            product.SetPrice(price);

            if (isDeleted)
            {
                product.MarkAsDeleted();
            }

            var validationResult = this.ValidateProduct(product);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            await this.productRepository.SaveChangesAsync();

            return new Result<Product>(product);
        }

        public async Task<Result<Product>> GetProductAsync(Guid id)
        {
            var product = await this.productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new Result<Product>($"Product {id} is not found");
            }

            return new Result<Product>(product);
        }

        public async Task<Result<Product>> GetProductAsync(string name)
        {
            var product = await this.productRepository.GetByNameAsync(name);
            if (product == null)
            {
                return new Result<Product>($"Product {name} is not found");
            }

            return new Result<Product>(product);
        }

        public async Task<Result<Product>> CreateOrUpdateDiscountAsync(Guid productId, int quantity, decimal discountPrice)
        {
            var result = await this.GetProductAsync(productId);
            if (result.IsSuccess == false)
            {
                return result;
            }

            var product = result.Value;
            product.SetDiscount(new Discount(productId, quantity, discountPrice));

            var validationResult = this.ValidateProduct(product);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            await this.productRepository.SaveChangesAsync();

            return new Result<Product>(product);
        }

        private Result<Product> ValidateProduct(Product product)
        {
            var notification = new Notification();
            product.Validate(notification);

            if (!product.IsValid)
            {
                return new Result<Product>(notification.ErrorsAsString);
            }

            return new Result<Product>(product);
        }
    }
}
