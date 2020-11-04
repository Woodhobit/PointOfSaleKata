using POS.Domain.OrderAggregate;
using POS.Domain.ProductAggregate;
using POS.Infrastructure.Repositories;
using System;

namespace POS.FunctionalTests.Common
{
    public class DataFixture : IDisposable
    {
        public IOrderRepository<Guid> OrderRepository { get; private set; }
        public IProductRepository<Guid> ProductRepository { get; private set; }

        public DataFixture()
        {
            this.OrderRepository = new InMemoryOrderRepositoryAsync();
            this.ProductRepository = new InMemoryProductRepositoryAsync();
        }

        public void Dispose()
        {
            this.OrderRepository = null;
            this.ProductRepository = null;
        }
    }
}
