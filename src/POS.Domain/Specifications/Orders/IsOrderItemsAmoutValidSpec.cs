using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;

namespace POS.Domain.Specifications.Orders
{
    class IsOrderItemsAmoutValidSpec : CompositeSpecification<Order>
    {
        public override bool IsSatisfiedBy(Order candidate)
        {
            return candidate.Items.Count >= 0;
        }
    }
}
