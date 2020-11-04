using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;
using POS.Domain.Specifications.Orders;

namespace POS.Domain.Validators
{
    class OrderValidator : Validator<Order>
    {
        public override void Validate(Notification note, Order entity)
        {
            CheckRule<IsCreatedDateValidSpec>(note, entity, "Empty Created date");
            CheckRule<IsCustomerValidSpec>(note, entity, "Ivalid customer ID");
            CheckRule<IsOrderItemsAmoutValidSpec>(note, entity, "Ivalid quantity");
        }
    }
}
