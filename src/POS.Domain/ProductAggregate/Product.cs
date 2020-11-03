using POS.Domain.SeedWork;
using System;

namespace POS.Domain.ProductAggregate
{
    public class Product : BaseEntity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public bool IsDeleted { get; private set; }
        public Discount Discount { get; private set; }

        public Product(string name, double price)
        {
            this.Name = name;
            this.Price = price;
            this.IsDeleted = false;
        }

        public void SetPrice(double price)
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
    }
}
