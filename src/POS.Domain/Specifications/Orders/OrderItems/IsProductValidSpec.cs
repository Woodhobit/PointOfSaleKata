using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;
using System;

namespace POS.Domain.Specifications.Orders.OrderItems
{
    public class IsProductValidSpec : CompositeSpecification<OrderItem>
    {
        public override bool IsSatisfiedBy(OrderItem candidate)
        {
            return candidate.ProductId != Guid.Empty;
        }
    }
}
