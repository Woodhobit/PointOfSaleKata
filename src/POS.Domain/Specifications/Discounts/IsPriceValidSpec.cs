using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;

namespace POS.Domain.Specifications.Dicounts
{
    public class IsPriceValidSpec : CompositeSpecification<Discount>
    {
        public override bool IsSatisfiedBy(Discount candidate)
        {
            return candidate.DiscountPrice > 0;
        }
    }
}
