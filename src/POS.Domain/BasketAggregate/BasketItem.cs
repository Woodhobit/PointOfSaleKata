using POS.Domain.SeedWork;
using System;

namespace POS.Domain.BasketAggregate
{
    public class BasketItem : BaseEntity<Guid>
    {
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public Guid ProductId { get; private set; }

        public BasketItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            SetQuantity(quantity);
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
