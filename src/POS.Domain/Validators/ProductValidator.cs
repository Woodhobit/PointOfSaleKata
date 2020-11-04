using POS.Domain.ProductAggregate;
using POS.Domain.SeedWork;
using POS.Domain.Specifications.Products;

namespace POS.Domain.Validators
{
    class ProductValidator : Validator<Product>
    {
        public override void Validate(Notification note, Product entity)
        {
            CheckRule<IsNameMaxLengthValidSpec>(note, entity, "Ivalid name's lenght");
            CheckRule<IsPriceValidSpec>(note, entity, "Ivalid price");
        }
    }
}
