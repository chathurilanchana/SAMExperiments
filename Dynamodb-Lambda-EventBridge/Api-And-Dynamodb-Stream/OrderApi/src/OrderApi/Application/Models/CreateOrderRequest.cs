using OrderApi.Infrastructure;

namespace OrderApi.Application.Models
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        public List<OrderItemsData> OrderItems { get; set; }

        public class OrderItemsData
        {
            public string Type { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}
