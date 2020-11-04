using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;

namespace POS.Domain.Specifications.Products
{
    public class IsNameMaxLengthValidSpec : CompositeSpecification<Product>
    {
        public override bool IsSatisfiedBy(Product candidate)
        {
            return !string.IsNullOrEmpty(candidate.Name) && candidate.Name?.Length <= 255;
        }
    }
}
