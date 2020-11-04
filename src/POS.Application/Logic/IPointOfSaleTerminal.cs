using System;
using System.Threading.Tasks;

namespace POS.Application.Logic
{
    public interface IPointOfSaleTerminal
    {
        Task<decimal> CalculateTotal();
        Task CancelOrder();
        Task DeleteItemsFromOrder();
        Task InitNewOrder(Guid customerId);
        Task Scan(string name);
    }
}