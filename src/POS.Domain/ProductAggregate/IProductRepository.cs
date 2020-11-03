using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Domain.ProductAggregate
{
    public interface IProductRepository<T>
    {
        Task<List<Product>> GetByIdsAsync(List<T> ids);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetById(T id);
    }
}
