using POS.Domain.ProductAggregate;
using POS.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Infrastructure.Domain.Products
{
    public class ProductRepository : IProductRepository<Guid>
    {
        private readonly IProductsContext context;

        public ProductRepository(IProductsContext context)
        {
            this.context = context;
        }

        public Task<Product> AddAsync(Product product)
        {
            this.context.Products.Add(product);
            return Task.FromResult(product);
        }

        public Task<List<Product>> GetAllAsync()
        {
            return Task.FromResult(this.context.Products.ToList());
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            return Task.FromResult(this.context.Products.FirstOrDefault(x => x.Id == id));
        }

        public Task<Product> GetByNameAsync(string name)
        {
            return Task.FromResult(this.context.Products.FirstOrDefault(x => x.Name == name));
        }

        public Task<List<Product>> GetByIdsAsync(List<Guid> ids)
        {
            return Task.FromResult(this.context.Products.Where(x => ids.Contains(x.Id)).ToList());
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }
    }
}
