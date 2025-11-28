using LegacyOrderService.Domain.Models;

namespace LegacyOrderService.Domain.Interfaces
{
    /// <summary>
    /// Abstraction over persistence for orders.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Persists an order.
        /// </summary>
        void Save(Order order);
    }
}
