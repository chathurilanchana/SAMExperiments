namespace OrderApi.Domain.Entities
{
    public class Order
    {
        public Order(string customerId, List<OrderItem> orderItems)
        {
            if (orderItems == null || orderItems.Count == 0)
                throw new ArgumentNullException("Order items null or empty");
            {
                Id = Guid.NewGuid().ToString();
                CreatedDate = DateTime.Now;
                CustomerId = customerId;
                OrderItems = orderItems;
            }
            
        }

        // To be used by Db to Entity mapper to construct domain object
        // You need to modify here if you add a new property to entity
        public Order(string customerId, string id, DateTime createdDate, List<OrderItem> orderItems, OrderStatus orderStatus)
        {
            Id = id;
            CustomerId=customerId;
            CreatedDate = createdDate;
            OrderItems = orderItems.ToList();
            Status = orderStatus;
        }

        public string CustomerId { get; private set; }
        public string Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public OrderStatus Status { get; private set; }

        public void AddItem(OrderItem item)
        {
            // Add rules here s:t if already exist increament count, otherwise add new one
            OrderItems.Add(item);
        }

        public void Ship()
        {
            Status = OrderStatus.Shipped;
        }

        public void Cancel()
        {
            Status=OrderStatus.Cancelled;
        }

        public void Return()
        {
            Status = OrderStatus.Returned;
        }
    }


    public enum OrderStatus
    {
        Created,
        Cancelled,
        Shipped,
        Returned
    }
}
