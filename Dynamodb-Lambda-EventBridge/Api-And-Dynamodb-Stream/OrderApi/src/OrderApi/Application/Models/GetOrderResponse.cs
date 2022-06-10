namespace OrderApi.Application.Models
{
    public class GetOrderResponse
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItemData> OrderItems { get; set; }

        public class OrderItemData
        {
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}
