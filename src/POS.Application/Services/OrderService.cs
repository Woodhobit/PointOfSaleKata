using POS.Application.Common;
using POS.Domain.OrderAggregate;
using POS.Domain.ProductAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductRepository<Guid> productRepository;
        private readonly IOrderRepository<Guid> orderRepository;

        public OrderService(IProductRepository<Guid> productRepository, IOrderRepository<Guid> orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public async Task<Result<Order>> CreateOrder(Guid customerId)
        {
            var basket = new Order(customerId);
            await this.orderRepository.AddAsync(basket);
            await orderRepository.SaveChangesAsync();

            return new Result<Order>(basket);
        }

        public async Task<Result<Order>> AddItemToOrderAsync(Guid orderId, Guid productId, int quantity = 1)
        {
            //ToDo validation

            var product = await this.productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return new Result<Order>($"Product {productId} is not found");
            }

            var result = await this.GetOrderByIdAsync(orderId);
            if (!result.IsSuccess)
            {
                return result;
            }

            var order = result.Value;
            order.AddItem(productId, quantity);
            await orderRepository.SaveChangesAsync();

            return new Result<Order>(order);
        }

        public async Task<Result<Order>> CancelOrderAsync(Guid orderId)
        {
            var result = await this.GetOrderByIdAsync(orderId);
            if (!result.IsSuccess)
            {
                return result;
            }

            var order = result.Value;
            order.Cancel();
            await orderRepository.SaveChangesAsync();

            return new Result<Order>(order);
        }

        public async Task<Result<Order>> RemoveOrderItemsAsync(Guid orderId)
        {
            var result = await this.GetOrderByIdAsync(orderId);
            if (!result.IsSuccess)
            {
                return result;
            }

            var order = result.Value;
            order.Clean();
            await orderRepository.SaveChangesAsync();

            return new Result<Order>(order);
        }

        private async Task<Result<Order>> GetOrderByIdAsync(Guid orderId)
        {
            var order = await this.orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return new Result<Order>($"Order {orderId} is not found");
            }

            return new Result<Order>(order);
        }

        public async Task<Result<decimal>> CalculateTotalAsync(Guid orderId)
        {
            var result = await this.GetOrderByIdAsync(orderId);
            if (!result.IsSuccess)
            {
                return new Result<decimal>(result.Error);
            }

            var order = result.Value;
            var groupedItems = order.Items.GroupBy(x => x.ProductId);

            decimal total = 0;
            foreach (var group in groupedItems)
            {
                var product = await this.productRepository.GetByIdAsync(group.Key);

                total = +this.CalculateTotalForProduct(product.Discount, product.Price, group.Count());
            }

            return new Result<decimal>(total);
        }

        private decimal CalculateTotalForProduct(Discount discount, decimal price, int quantity)
        {
            var discountQuantity = discount.Quantity;
            var discoutPrice = discount.DiscountPrice;

            if (quantity < discountQuantity)
            {
                return price * quantity;
            }

            var notDiscountedPrice = (quantity % discountQuantity) * price;
            var discountedPrice = (quantity / discountQuantity) * discoutPrice;

            return notDiscountedPrice + discountedPrice;
        }

    }
}
