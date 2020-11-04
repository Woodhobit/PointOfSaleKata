using Microsoft.Extensions.DependencyInjection;
using POS.Application.Logic;
using POS.Application.Services;
using POS.Domain.OrderAggregate;
using POS.Domain.ProductAggregate;
using POS.Infrastructure.Repositories;
using System;

namespace POS.Client.Helpers
{
    public static class DIHelper
    {
        public static ServiceProvider Build()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IOrderRepository<Guid>, InMemoryOrderRepositoryAsync>()
                .AddSingleton<IProductRepository<Guid>, InMemoryProductRepositoryAsync>()
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IPointOfSaleTerminal, PointOfSaleTerminal>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
