using POS.Application.Common;
using POS.Domain.OrderAggregate;
using POS.Domain.ProductAggregate;
using System;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public interface IOrderService
    {
        Task<Result<Order>> AddItemToOrderAsync(Guid id, Guid productId, int quantity = 1);
        Task<Result<decimal>> CalculateTotalAsync(Guid id);
        Task<Result<Order>> CreateOrder();
        Task DeleteOrderAsync(Guid basketId);
    }
}