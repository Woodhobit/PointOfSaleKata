using POS.Domain.SeedWork;
using System;

namespace POS.Domain.OrderAggregate
{
    public class OrderItem : BaseEntity<Guid>
    {
        public int Quantity { get; private set; }
        public Guid ProductId { get; private set; }

        public OrderItem(Guid productId, int quantity)
        {
            ProductId = productId;
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
