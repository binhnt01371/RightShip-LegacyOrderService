using LegacyOrderService.Domain.Models;

namespace LegacyOrderService.Application.CQRS.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public Order Order { get; }
        public CreateOrderCommand(Order order) => Order = order;
    }
}
