using POS.Application.Common;
using POS.Domain.ProductAggregate;
using System;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository<Guid> productRepository;

        public ProductService(IProductRepository<Guid> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Result<Product>> CreateProductAsync(string name, decimal price)
        {
            //ToDo validation

            var existed =  await this.productRepository.GetByNameAsync(name);
            if(existed != null)
            {
                return new Result<Product>($"Product {name} is already added");
            }

            var product = new Product(name, price);

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();

            return new Result<Product>(product);
        }

        public async Task<Result<Product>> UpdateProductAsync(Guid id, string name, decimal price, bool isDeleted = false)
        {
            //ToDo validation

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

        public async Task<Result<Product>> CreateOrUpdateDiscountAsync(Guid productId,int quantity, decimal discountPrice)
        {
            var result = await this.GetProductAsync(productId);
            if (result.IsSuccess == false)
            {
                return result;
            }

            var product = result.Value;
            product.SetDiscount(new Discount(productId, quantity, discountPrice));

            await this.productRepository.SaveChangesAsync();

            return new Result<Product>(product);
        }
    }
}
