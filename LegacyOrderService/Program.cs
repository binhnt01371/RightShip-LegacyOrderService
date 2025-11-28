using System;
using Microsoft.Extensions.DependencyInjection;
using LegacyOrderService.Application.CQRS;
using LegacyOrderService.Application.CQRS.Commands;
using LegacyOrderService.Application.CQRS.Queries;
using LegacyOrderService.Domain.Interfaces;
using LegacyOrderService.Domain.Models;
using LegacyOrderService.Infrastructure.Repositories;

namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            // Register repositories
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            // Register CQRS handlers
            services.AddTransient<IRequestHandler<GetProductPriceQuery, double>, GetProductPriceQueryHandler>();
            services.AddTransient<IRequestHandler<CreateOrderCommand, bool>, CreateOrderCommandHandler>();

            // Register mediator
            services.AddSingleton<Mediator>();

            var provider = services.BuildServiceProvider();
            var mediator = new Mediator(provider);

            Console.WriteLine("Welcome to Order Processor!");
            Console.WriteLine("Enter customer name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter product name:");
            string product = Console.ReadLine();

            double price = mediator.Send(new GetProductPriceQuery(product));

            Console.WriteLine("Enter quantity:");
            int qty = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Processing order...");

            var order = new Order
            {
                CustomerName = name,
                ProductName = product,
                Quantity = qty,
                Price = price
            };

            double total = order.Quantity * order.Price;

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + total);

            Console.WriteLine("Saving order to database...");
            mediator.Send(new CreateOrderCommand(order));
            Console.WriteLine("Done.");
        }
    }
}
