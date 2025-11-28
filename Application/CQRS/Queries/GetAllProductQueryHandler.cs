using LegacyOrderService.Application.CQRS;
using LegacyOrderService.Domain.Interfaces;

namespace LegacyOrderService.Application.CQRS.Queries
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<string>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<string> Handle(GetAllProductQuery request)
        {
            return _productRepository.GetAllProducts();
        }
    }
}
