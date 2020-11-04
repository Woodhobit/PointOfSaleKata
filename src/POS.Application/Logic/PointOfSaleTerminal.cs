using POS.Application.Exception;
using POS.Application.Services;
using POS.Domain.OrderAggregate;
using System;
using System.Threading.Tasks;

namespace POS.Application.Logic
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private Order order;

        public PointOfSaleTerminal(IProductService productService, IOrderService orderService)
        {
            this.productService = productService;
            this.orderService = orderService;
        }

        public async Task InitNewOrder(Guid customerId)
        {
            var result = await this.orderService.CreateOrder(customerId);

            if (!result.IsSuccess) throw new POSException(result.Error);

            this.order = result.Value;
        }

        public async Task Scan(string name)
        {
            var result = await this.productService.GetProductAsync(name);

            if (!result.IsSuccess) throw new POSException(result.Error);

            await this.orderService.AddItemToOrderAsync(this.order.Id, result.Value.Id);
        }

        public async Task<decimal> CalculateTotal()
        {
            var result = await this.orderService.CalculateTotalAsync(this.order.Id);

            if (!result.IsSuccess) throw new POSException(result.Error);

            return result.Value;
        }

        public Task CancelOrder()
        {
            return this.orderService.CancelOrderAsync(this.order.Id);
        }

        public Task DeleteItemsFromOrder()
        {
            return this.orderService.RemoveOrderItemsAsync(this.order.Id);
        }
    }
}
