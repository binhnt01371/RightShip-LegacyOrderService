using LegacyOrderService.Domain.Exceptions;
using LegacyOrderService.Domain.Interfaces;

namespace LegacyOrderService.Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly Dictionary<string, double> _productPrices = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Widget"] = 12.99,
            ["Gadget"] = 15.49,
            ["Doohickey"] = 8.75
        };

        public double GetPrice(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name must be provided", nameof(productName));

            if (_productPrices.TryGetValue(productName, out var price))
                return price;

            throw new ProductNotFoundException($"Product '{productName}' not found");
        }
    }
}
