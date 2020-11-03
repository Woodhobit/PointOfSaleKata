using System.Threading.Tasks;

namespace POS.Domain.BasketAggregate
{
    public interface IBasketRepository<T>
    {
        Task<Basket> GetByIdAsync(T id);
        Task<Basket> AddAsync(Basket basket);
        Task DeleteAsync(Basket basket);
        Task SaveChangesAsync();
    }
}
