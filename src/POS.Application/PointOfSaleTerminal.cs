using POS.Application.Services;
using POS.Domain.OrderAggregate;
using System.Threading.Tasks;

namespace POS.Application
{
    public class PointOfSaleTerminal
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private Order order;

        public async Task Start()
        {
            var result = await this.orderService.CreateOrder();
            this.order = result.Value;
        }

        public async Task Scan(string name)
        {
            var result = await this.productService.GetProductAsync(name);

            await this.orderService.AddItemToOrderAsync(this.order.Id, result.Value.Id);
        }

        public async Task<decimal> CalculateTotal() 
        {
            var result = await this.orderService.CalculateTotalAsync(this.order.Id);
            return result.Value;
        }
    }
}
