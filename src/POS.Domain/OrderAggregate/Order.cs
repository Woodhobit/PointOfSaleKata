﻿using POS.Domain.SeedWork;
using POS.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POS.Domain.OrderAggregate
{
    public class Order : BaseEntity<Guid>, IAggregateRoot
    {
        private readonly List<OrderItem> items = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> Items => this.items.AsReadOnly();
        public Guid CustomerId { get; private set; }
        public DateTimeOffset Created { get; private set; }
        public bool Canceled { get;  private set; }
        public bool IsValid { get; private set; }

        public Order(Guid customerId)
        {
            this.Id = Guid.NewGuid();
            this.Created = DateTimeOffset.Now;
            this.Canceled = false;
            this.CustomerId = customerId;
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

        public void Cancel()
        {
            this.Canceled = true;
        }

        public void Validate(Notification note)
        {
            var validator = new OrderValidator();
            validator.Validate(note, this);

            foreach (var orderItem in this.items)
            {
                orderItem.Validate(note);
            }

            IsValid = !note.HasErrors;
        }
    }

}
