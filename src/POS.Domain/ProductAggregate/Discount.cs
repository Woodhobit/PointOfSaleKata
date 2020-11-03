using POS.Domain.SeedWork;
using System;

namespace POS.Domain.ProductAggregate
{
    public class Discount : BaseEntity<Guid>
    {
        public int Quantity { get; private set; }
        public double DiscountPrice { get; private set; }

        public Discount(int quantity, double discountPrice)
        {
            this.Quantity = quantity;
            this.DiscountPrice = discountPrice;
        }

        public void SetQuantity(int quantity)
        {
            this.Quantity = quantity;
        }

        public void SetDiscountPrice(double discountPrice)
        {
            this.DiscountPrice = discountPrice;
        }
    }
}
