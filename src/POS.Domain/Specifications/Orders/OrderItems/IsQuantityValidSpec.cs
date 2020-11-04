using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;

namespace POS.Domain.Specifications.Orders.OrderItems
{
    public class IsQuantityValidSpec : CompositeSpecification<OrderItem>
    {
        public override bool IsSatisfiedBy(OrderItem candidate)
        {
            return candidate.Quantity >= 1;
        }
    }
}
