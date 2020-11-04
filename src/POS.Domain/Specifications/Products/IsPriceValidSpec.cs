using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;

namespace POS.Domain.Specifications.Products
{
    public class IsPriceValidSpec : CompositeSpecification<Product>
    {
        public override bool IsSatisfiedBy(Product candidate)
        {
            return candidate.Price > 0;
        }
    }
}
