using OrderApi.Application.Models;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure;
using OrderApi.Repositories;
using OrderApi.Repositories.Mappers;

namespace OrderApi.Application.Handlers
{
    public class GetLatestActiveOrderHandler : IQueryHandler<string, GetOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetLatestActiveOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderResponse> HandleAsync(string userId)
        {
            var orderDocument = await _orderRepository.GetlatestActiveOrder(userId, OrderStatus.Created.ToString());

            var orderId = OrderEntityToDbMapper.GetOrderId(orderDocument);

            if(orderId == null)
            {
                return null;
            }
            var orderLinesDocuments = await _orderRepository.GetOrderLines(userId, orderId);

            var mappedOrder = OrderEntityToDbMapper.MapOrderAndOrderLineDocumentsToOrderEntity(orderDocument, orderLinesDocuments);

            return new GetOrderResponse
            {
                CreatedDate = mappedOrder.CreatedDate,
                Id = mappedOrder.Id,
                OrderItems = mappedOrder.OrderItems.Select(p => new GetOrderResponse.OrderItemData { Price = p.Price, Quantity = p.Quantity }).ToList()
            };
        }
    }
}
