namespace LegacyOrderService.Domain.Interfaces
{
    /// <summary>
    /// Repository abstraction for product pricing.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets the price for the specified product or throws if not found.
        /// </summary>
        double GetPrice(string productName);
    }
}
