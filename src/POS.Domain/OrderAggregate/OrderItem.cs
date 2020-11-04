using POS.Domain.SeedWork;
using POS.Domain.Validators;
using System;

namespace POS.Domain.OrderAggregate
{
    public class OrderItem : BaseEntity<Guid>
    {
        public int Quantity { get; private set; }
        public Guid ProductId { get; private set; }
        public bool IsValid { get; private set; }

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

        public void Validate(Notification note)
        {
            var validator = new OrderItemValidator();
            validator.Validate(note, this);

            this.IsValid = !note.HasErrors;
        }
    }
}
