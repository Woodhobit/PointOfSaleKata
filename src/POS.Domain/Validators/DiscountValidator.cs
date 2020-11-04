using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;
using POS.Domain.Specifications.Dicounts;

namespace POS.Domain.Validators
{
    class DiscountValidator : Validator<Discount>
    {
        public override void Validate(Notification note, Discount entity)
        {
            CheckRule<IsQuantityValidSpec>(note, entity, "Ivalid quantity");
            CheckRule<IsPriceValidSpec>(note, entity, "Ivalid price");
        }
    }
}
