using System.Threading.Tasks;

namespace POS.Domain.OrderAggregate
{
    public interface IOrderRepository<T>
    {
        Task<Order> GetByIdAsync(T id);
        Task<Order> AddAsync(Order basket);
        Task DeleteAsync(Order basket);
        Task SaveChangesAsync();
    }
}
