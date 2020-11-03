using POS.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POS.Domain.OrderAggregate
{
    public class Order : BaseEntity<Guid>, IAggregateRoot
    {
        private readonly List<OrderItem> items = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> Items => this.items.AsReadOnly();

        public Order()
        {
            this.Id = Guid.NewGuid();
        }

        public void AddItem(Guid productId, int quantity = 1)
        {
            if (!this.items.Any(i => i.ProductId == productId))
            {
                this.items.Add(new OrderItem(productId, quantity));
                return;
            }

            var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);
            existingItem.AddQuantity(quantity);
        }

        public void RemoveEmptyItems()
        {
            this.items.RemoveAll(i => i.Quantity == 0);
        }

        public void Clean()
        {
            this.items.Clear();
        }
    }

}
