using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;
using System;

namespace POS.Domain.Specifications.Orders
{
    public class IsCustomerValidSpec : CompositeSpecification<Order>
    {
        public override bool IsSatisfiedBy(Order candidate)
        {
            return candidate.CustomerId != Guid.Empty;
        }
    }
}
