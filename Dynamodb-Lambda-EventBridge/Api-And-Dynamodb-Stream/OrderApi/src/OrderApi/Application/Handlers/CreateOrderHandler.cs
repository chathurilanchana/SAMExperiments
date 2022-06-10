using OrderApi.Application.Models;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure;
using OrderApi.Repositories;
using OrderApi.Repositories.Mappers;

namespace OrderApi.Application.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderRequest>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(CreateOrderRequest command)
        {
            var orderItems = command.OrderItems.Select(p => new OrderItem(p.Type, p.Price, p.Quantity));

            var orders = new List<Order>();

            var order = new Order(command.UserId, orderItems.ToList());

            var documentsToSave = OrderEntityToDbMapper.MapOrderEntityToDynamoDBDocuments(order);

            await _orderRepository.AddOrderAndOrderLines(documentsToSave);
        }
    }
}
