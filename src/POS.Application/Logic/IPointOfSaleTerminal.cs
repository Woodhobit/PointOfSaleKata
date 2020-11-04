using System;
using System.Threading.Tasks;

namespace POS.Application.Logic
{
    public interface IPointOfSaleTerminal
    {
        Task<decimal> CalculateTotalAsync();
        Task CancelOrderAsync();
        Task DeleteItemsFromOrderAsync();
        Task InitNewOrderAsync(Guid customerId);
        Task ScanAsync(string name);
    }
}