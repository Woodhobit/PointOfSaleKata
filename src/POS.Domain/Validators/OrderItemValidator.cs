using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;
using POS.Domain.Specifications.Orders.OrderItems;

namespace POS.Domain.Validators
{
    public class OrderItemValidator : Validator<OrderItem>
    {
        public override void Validate(Notification note, OrderItem entity)
        {
            CheckRule<IsProductValidSpec>(note, entity, "Empty product ID");
            CheckRule<IsQuantityValidSpec>(note, entity, "Ivalid quantity");
        }
    }
}
