using POS.Domain.ProductAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistence
{
    public interface IProductsContext
    {
        IQueryable<Discount> Discounts { get; set; }
        IQueryable<Product> Products { get; set; }
        Task SaveChanges();
    }
}
