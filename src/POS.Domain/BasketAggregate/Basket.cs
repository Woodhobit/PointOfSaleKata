using POS.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POS.Domain.BasketAggregate
{
    public class Basket : BaseEntity<Guid>, IAggregateRoot
    {
        private readonly List<BasketItem> items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => this.items.AsReadOnly();

        public void AddItem(Guid productId, decimal unitPrice, int quantity = 1)
        {
            if (!this.items.Any(i => i.ProductId == productId))
            {
                this.items.Add(new BasketItem(productId, quantity, unitPrice));
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
