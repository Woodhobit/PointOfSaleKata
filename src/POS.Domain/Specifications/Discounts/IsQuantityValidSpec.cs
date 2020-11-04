using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;

namespace POS.Domain.Specifications.Dicounts
{
    public class IsQuantityValidSpec : CompositeSpecification<Discount>
    {
        public override bool IsSatisfiedBy(Discount candidate)
        {
            return candidate.Quantity >= 1;
        }
    }
}
