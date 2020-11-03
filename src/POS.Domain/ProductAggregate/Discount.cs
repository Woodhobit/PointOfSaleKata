using POS.Domain.SeedWork;
using System;

namespace POS.Domain.ProductAggregate
{
    public class Discount : BaseEntity<Guid>
    {
        public int Quantity { get; private set; }
        public decimal DiscountPrice { get; private set; }
        public Guid ProductId { get; private set; }

        public Discount(Guid productId, int quantity, decimal discountPrice)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
            this.DiscountPrice = discountPrice;
        }

        public void SetQuantity(int quantity)
        {
            this.Quantity = quantity;
        }

        public void SetDiscountPrice(decimal discountPrice)
        {
            this.DiscountPrice = discountPrice;
        }
    }
}
