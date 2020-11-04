using POS.Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Infrastructure.Repositories
{
    public class InMemoryProductRepositoryAsync : IProductRepository<Guid>
    {
        private List<Product> products;

        public InMemoryProductRepositoryAsync()
        {
            this.SetupMock();
        }

        public Task<Product> AddAsync(Product product)
        {
            this.products.Add(product);
            return Task.FromResult(product);
        }

        public Task<List<Product>> GetAllAsync()
        {
            return Task.FromResult(this.products.ToList());
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            return Task.FromResult(this.products.FirstOrDefault(x => x.Id == id));
        }

        public Task<Product> GetByNameAsync(string name)
        {
            return Task.FromResult(this.products.FirstOrDefault(x => x.Name == name));
        }

        public Task<List<Product>> GetByIdsAsync(List<Guid> ids)
        {
            return Task.FromResult(this.products.Where(x => ids.Contains(x.Id)).ToList());
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        private void SetupMock()
        {
            var productA = new Product("A", 1.25m);
            var productB = new Product("B", 4.25m);
            var productC = new Product("C", 1m);
            var productD = new Product("D", 0.75m);

            var discountProductA = new Discount(productA.Id, 3, 3);
            productA.SetDiscount(discountProductA);

            var discountProductC = new Discount(productC.Id, 6, 5);
            productC.SetDiscount(discountProductC);

            this.products = new List<Product> { productA, productB, productC, productD };
        }
    }
}
