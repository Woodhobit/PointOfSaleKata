using POS.Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistence
{
    public class InMemoryProductsContext : IProductsContext
    {
        private List<Discount> discounts;
        private List<Product> products;

        public InMemoryProductsContext()
        {
            this.SetupMock();
        }

        public List<Discount> Discounts { get => this.discounts; set => this.discounts = value; }
        public List<Product> Products { get => this.products; set => this.products = value; }

        public async Task SaveChanges()
        {
            await Task.CompletedTask;
        }

        private void SetupMock()
        {
            var productA = new Product("A", 1.25m);
            var productB = new Product("B", 4.25m);
            var productC = new Product("C", 1m);
            var productD = new Product("D", 0.25m);

            var discountProductA = new Discount(productA.Id, 3, 3);
            productA.SetDiscount(discountProductA);

            var discountProductC = new Discount(productC.Id, 6, 5);
            productC.SetDiscount(discountProductC);

            this.discounts = new List<Discount> { discountProductA, discountProductC };

            this.products = new List<Product> { productA, productB, productC, productD };
        }
    }
}
