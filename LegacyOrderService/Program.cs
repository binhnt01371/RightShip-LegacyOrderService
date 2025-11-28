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
            services.AddTransient<IRequestHandler<GetAllProductQuery, IEnumerable<string>>, GetAllProductQueryHandler>();
            services.AddTransient<IRequestHandler<CreateOrderCommand, bool>, CreateOrderCommandHandler>();

            // Register mediator
            services.AddSingleton<Mediator>();

            var provider = services.BuildServiceProvider();
            var mediator = new Mediator(provider);

            Console.WriteLine("Welcome to Order Processor!");
            Console.WriteLine("Enter customer name:");
            var name = Console.ReadLine();

            var products = mediator.Send(new GetAllProductQuery());
            var product = SelectProduct(products);

            var price = mediator.Send(new GetProductPriceQuery(product));
            int qty = EnterQuantity();

            Console.WriteLine("Processing order...");

            var order = new Order
            {
                CustomerName = name,
                ProductName = product,
                Quantity = qty,
                Price = price
            };

            var total = order.Quantity * order.Price;

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + total);

            Console.WriteLine("Saving order to database...");
            mediator.Send(new CreateOrderCommand(order));
            Console.WriteLine("Done.");
        }

        private static int EnterQuantity()
        {
            Console.WriteLine("Enter quantity:");
            if (int.TryParse(Console.ReadLine(), out int qty))
            {
                return qty;
            }
            Console.WriteLine("Invalid quantity. Please enter a valid integer."); 
            Console.WriteLine();
            return EnterQuantity();
        }

        private static string SelectProduct(IEnumerable<string> products)
        {
            Console.WriteLine("Select product:");
            foreach (var prod in products)
            {
                Console.WriteLine("- " + prod);
            }
            Console.Write("Enter your choice: ");
            var product = Console.ReadLine();
            if (!products.Contains(product, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid product selected. Please try again.");
                Console.WriteLine();
                return SelectProduct(products);
            }
            return product;
        }
    }
}
