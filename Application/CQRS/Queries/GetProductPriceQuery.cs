namespace LegacyOrderService.Application.CQRS.Queries
{
    public class GetProductPriceQuery : IRequest<double>
    {
        public string ProductName { get; }
        public GetProductPriceQuery(string productName) => ProductName = productName;
    }
}
