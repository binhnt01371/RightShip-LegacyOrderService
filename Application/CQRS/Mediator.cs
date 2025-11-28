namespace LegacyOrderService.Application.CQRS
{
    public class Mediator
    {
        private readonly IServiceProvider _provider;

        public Mediator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _provider.GetService(handlerType);
            if (handler == null) throw new InvalidOperationException($"No handler registered for {request.GetType()}");

            var method = handlerType.GetMethod("Handle");
            return (TResponse)method.Invoke(handler, new object[] { request });
        }
    }
}
