using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Domain.ProductAggregate
{
    public interface IProductRepository<T>
    {
        Task<List<Product>> GetByIdsAsync(List<T> ids);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(T id);
        Task<Product> GetByNameAsync(string name);
        Task<Product> AddAsync(Product product);
        Task SaveChangesAsync();
    }
}
