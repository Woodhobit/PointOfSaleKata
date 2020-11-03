using POS.Application.Common;
using POS.Domain.BasketAggregate;
using POS.Domain.ProductAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public class BasketService
    {
        private readonly IProductRepository<Guid> productRepository;
        private readonly IBasketRepository<Guid> basketRepository;

        public BasketService(IProductRepository<Guid> productRepository, IBasketRepository<Guid> basketRepository)
        {
            this.productRepository = productRepository;
            this.basketRepository = basketRepository;
        }

        public async Task<Result<Basket>> CreateBasket()
        {
            var basket = new Basket();
            await this.basketRepository.AddAsync(basket);

            return new Result<Basket>(basket);
        }

        public async Task<Result<Basket>> AddItemToBasketAsync(Guid id, Guid productId, decimal price, int quantity = 1)
        {
            //ToDo validation

            var product = await this.productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return new Result<Basket>($"Product {productId} is not found");
            }

            var basket = await basketRepository.GetByIdAsync(id);
            if (basket == null)
            {
                return new Result<Basket>($"Basket {id} is not found");
            }

            basket.AddItem(productId, price, quantity);
            await basketRepository.SaveChangesAsync();

            return new Result<Basket>(basket);
        }

        public async Task DeleteBasketAsync(Guid basketId)
        {
            var basket = await this.basketRepository.GetByIdAsync(basketId);
            await this.basketRepository.DeleteAsync(basket);
        }

        public  async Task<Result<decimal>> CalculateTotal(Guid id)
        {
            var basket = await basketRepository.GetByIdAsync(id);
            if (basket == null)
            {
                return new Result<decimal>($"Basket {id} is not found");
            }

            var groupedItems = basket.Items.GroupBy(x => x.ProductId);

            decimal total = 0;
            foreach (var group in groupedItems)
            {
                var product = await this.productRepository.GetByIdAsync(group.Key);

                total =+ this.CalculateTotalForProduct(product.Discount, product.Price, group.Count());
            }

            return new Result<decimal>(total);
        }

        public decimal CalculateTotalForProduct(Discount discount, decimal price, int quantity)
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
