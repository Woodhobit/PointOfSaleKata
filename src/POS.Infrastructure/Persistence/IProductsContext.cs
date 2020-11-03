using POS.Domain.ProductAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistence
{
    public interface IProductsContext
    {
        List<Discount> Discounts { get; set; }
        List<Product> Products { get; set; }
        Task SaveChanges();
    }
}
