using POS.Domain.SeedWork;
using System;

namespace POS.Domain.ProductAggregate
{
    public class Discount : BaseEntity<Guid>
    {
        public int Quantity { get; private set; }
        public decimal DiscountPrice { get; private set; }

        public Discount(int quantity, decimal discountPrice)
        {
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
