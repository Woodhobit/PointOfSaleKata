using POS.Domain.SeedWork;
using POS.Domain.Validators;
using System;

namespace POS.Domain.ProductAggregate
{
    public class Product : BaseEntity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public bool IsDeleted { get; private set; }
        public Discount Discount { get; private set; }
        public bool IsValid { get; private set; }

        public Product(string name, decimal price)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Price = price;
            this.IsDeleted = false;
        }

        public void SetPrice(decimal price)
        {
            this.Price = price;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public void MarkAsDeleted()
        {
            this.IsDeleted = true;
        }

        public void SetDiscount(Discount discount)
        {
            this.Discount = discount;
        }

        public void Validate(Notification note)
        {
            var validator = new ProductValidator();

            validator.Validate(note, this);

            if (this.Discount != null)
            {
                this.Discount.Validate(note);
            }

            this.IsValid = !note.HasErrors;
        }
    }
}
