using LegacyOrderService.Application.CQRS;
using LegacyOrderService.Domain.Interfaces;

namespace LegacyOrderService.Application.CQRS.Queries
{
    public class GetProductPriceQueryHandler : IRequestHandler<GetProductPriceQuery, double>
    {
        private readonly IProductRepository _productRepository;

        public GetProductPriceQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public double Handle(GetProductPriceQuery request)
        {
            return _productRepository.GetPrice(request.ProductName);
        }
    }
}
