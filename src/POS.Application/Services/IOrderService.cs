using POS.Application.Common;
using POS.Domain.OrderAggregate;
using System;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public interface IOrderService
    {
        Task<Result<Order>> AddItemToOrderAsync(Guid orderId, Guid productId, int quantity = 1);
        Task<Result<decimal>> CalculateTotalAsync(Guid orderId);
        Task<Result<Order>> CancelOrderAsync(Guid orderId);
        Task<Result<Order>> CreateOrder(Guid customerId);
        Task<Result<Order>> RemoveOrderItemsAsync(Guid orderId);
    }
}