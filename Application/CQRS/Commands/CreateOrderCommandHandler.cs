using LegacyOrderService.Application.CQRS;
using LegacyOrderService.Domain.Interfaces;
using LegacyOrderService.Domain.Models;

namespace LegacyOrderService.Application.CQRS.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool Handle(CreateOrderCommand request)
        {
            _orderRepository.Save(request.Order);
            return true;
        }
    }
}
