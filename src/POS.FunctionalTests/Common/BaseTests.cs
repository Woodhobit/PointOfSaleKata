using POS.Application.Logic;
using POS.Application.Services;
using Xunit;

namespace POS.FunctionalTests.Common
{
    public class BaseTests :  IClassFixture<DataFixture>
    {
        public IOrderService OrderService { get; }
        public IProductService ProductService { get; }
        public IPointOfSaleTerminal PointOfSaleTerminal { get; }

        public BaseTests(DataFixture fixture)
        {
            this.OrderService = new OrderService(fixture.ProductRepository, fixture.OrderRepository);
            this.ProductService = new ProductService(fixture.ProductRepository);
            this.PointOfSaleTerminal = new PointOfSaleTerminal(this.ProductService, this.OrderService);
        }
    }
}
