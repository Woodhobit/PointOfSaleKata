using Microsoft.Extensions.DependencyInjection;
using POS.Application.Logic;
using POS.Client.Helpers;
using System;

namespace POS.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = DIHelper.Build();

            var terminal = serviceProvider.GetService<IPointOfSaleTerminal>();

            terminal.InitNewOrder(Guid.NewGuid());
            terminal.Scan("A");
            terminal.Scan("B");
            terminal.Scan("C");

            var result = terminal.CalculateTotal();

        }
    }
}
