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

        public async Task<Result<Order>> CreateOrder()
        {
            var basket = new Order();
            await this.orderRepository.AddAsync(basket);

            return new Result<Order>(basket);
        }

        public async Task<Result<Order>> AddItemToOrderAsync(Guid id, Guid productId, int quantity = 1)
        {
            //ToDo validation

            var product = await this.productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return new Result<Order>($"Product {productId} is not found");
            }

            var basket = await orderRepository.GetByIdAsync(id);
            if (basket == null)
            {
                return new Result<Order>($"Order {id} is not found");
            }

            basket.AddItem(productId, quantity);
            await orderRepository.SaveChangesAsync();

            return new Result<Order>(basket);
        }

        public async Task DeleteOrderAsync(Guid basketId)
        {
            var basket = await this.orderRepository.GetByIdAsync(basketId);
            await this.orderRepository.DeleteAsync(basket);
        }

        public async Task<Result<decimal>> CalculateTotalAsync(Guid id)
        {
            var basket = await orderRepository.GetByIdAsync(id);
            if (basket == null)
            {
                return new Result<decimal>($"Order {id} is not found");
            }

            var groupedItems = basket.Items.GroupBy(x => x.ProductId);

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
