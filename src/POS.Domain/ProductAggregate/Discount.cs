using POS.Domain.SeedWork;
using POS.Domain.Validators;
using System;

namespace POS.Domain.ProductAggregate
{
    public class Discount : BaseEntity<Guid>
    {
        public int Quantity { get; private set; }
        public decimal DiscountPrice { get; private set; }
        public Guid ProductId { get; private set; }
        public bool IsValid { get; private set; }

        public Discount(Guid productId, int quantity, decimal discountPrice)
        {
            this.Id = Guid.NewGuid();
            this.ProductId = productId;
            this.Quantity = quantity;
            this.DiscountPrice = discountPrice;
        }
        public Discount(Guid productId)
        {
            this.Id = Guid.NewGuid();
            this.ProductId = productId;
        }


        public void SetQuantity(int quantity)
        {
            this.Quantity = quantity;
        }

        public void SetDiscountPrice(decimal discountPrice)
        {
            this.DiscountPrice = discountPrice;
        }

        public void Validate(Notification note)
        {
            var validator = new DiscountValidator();

            validator.Validate(note, this);

            this.IsValid = !note.HasErrors;
        }
    }
}
