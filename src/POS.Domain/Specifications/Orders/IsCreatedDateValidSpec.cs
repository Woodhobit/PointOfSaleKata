﻿using POS.Domain.OrderAggregate;
using POS.Domain.SeedWork;

namespace POS.Domain.Specifications.Orders
{
    class IsCreatedDateValidSpec : CompositeSpecification<Order>
    {
        public override bool IsSatisfiedBy(Order candidate)
        {
            return candidate.Created != null;
        }
    }
}
