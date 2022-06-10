using OrderApi.Infrastructure;

namespace OrderApi.Application
{
    public interface IOrderService
    {
        public Task HandleAsync<TCommand>(TCommand request);
        public Task<TResponse> HandleAsync<TCommand, TResponse>(TCommand request);
    }
    public class OrderService : IOrderService
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task HandleAsync<TCommand>(TCommand command)
        {
            var hanlder = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await hanlder.HandleAsync(command);
        }

        public async Task<TResponse> HandleAsync<TQuery, TResponse>(TQuery query)
        {
            var hanlder = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();
            return await hanlder.HandleAsync(query);
        }
    }
}
