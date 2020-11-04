using Microsoft.Extensions.DependencyInjection;
using POS.Application.Logic;
using POS.Client.Helpers;
using System;
using System.Threading.Tasks;

namespace POS.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = DIHelper.Build();

            var terminal = serviceProvider.GetService<IPointOfSaleTerminal>();

            await terminal.InitNewOrderAsync(Guid.NewGuid());
            await terminal.ScanAsync("A");
            await terminal.ScanAsync("B");
            await terminal.ScanAsync("C");
            await terminal.ScanAsync("D");

            var result = await terminal.CalculateTotalAsync();
        }
    }
}
