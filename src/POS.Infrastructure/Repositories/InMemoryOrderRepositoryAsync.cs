using POS.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Infrastructure.Repositories
{
    public class InMemoryOrderRepositoryAsync : IOrderRepository<Guid>
    {
        private readonly List<Order> orders;

        public InMemoryOrderRepositoryAsync()
        {
            this.orders = new List<Order>();
        }

        public Task<Order> AddAsync(Order order)
        {
            this.orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order> GetByIdAsync(Guid id)
        {
            return Task.FromResult(this.orders.FirstOrDefault(x => x.Id == id));
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }
    }
}
