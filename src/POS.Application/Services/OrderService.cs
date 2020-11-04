using POS.Application.Common;
using POS.Domain.OrderAggregate;
using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;
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
            var order = new Order(customerId);
            var validationResult = this.ValidateOrder(order);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            await this.orderRepository.AddAsync(order);
            await orderRepository.SaveChangesAsync();

            return new Result<Order>(order);
        }

        public async Task<Result<Order>> AddItemToOrderAsync(Guid orderId, Guid productId, int quantity = 1)
        {
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

            return await this.ValidateOrderAndSaveAsync(order);
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

            return await this.ValidateOrderAndSaveAsync(order);
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

            return await this.ValidateOrderAndSaveAsync(order);
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

            decimal total = 0;
            foreach (var orderItem in order.Items)
            {
                var product = await this.productRepository.GetByIdAsync(orderItem.ProductId);

                total += this.CalculateTotalForProduct(orderItem, product);
            }

            return new Result<decimal>(total);
        }

        private decimal CalculateTotalForProduct(OrderItem orderItem, Product product)
        {
            var price = product.Price;
            var quantity = orderItem.Quantity;
            var discount = product.Discount;

            if (discount == null)
            {
                return price * quantity;
            }

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

        private async Task<Result<Order>> ValidateOrderAndSaveAsync(Order order)
        {
            var notification = new Notification();
            order.Validate(notification);

            if (!order.IsValid)
            {
                return new Result<Order>(notification.ErrorsAsString);
            }

            await orderRepository.SaveChangesAsync();

            return new Result<Order>(order);
        }

        private Result<Order> ValidateOrder(Order order)
        {
            var notification = new Notification();
            order.Validate(notification);

            if (!order.IsValid)
            {
                return new Result<Order>(notification.ErrorsAsString);
            }

            return new Result<Order>(order);
        }
    }
}
