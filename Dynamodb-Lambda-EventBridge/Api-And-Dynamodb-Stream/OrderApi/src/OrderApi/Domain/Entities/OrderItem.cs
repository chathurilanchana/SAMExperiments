namespace OrderApi.Domain.Entities
{
    public class OrderItem
    {
        public OrderItem(string type, decimal price, int quantity)
        {
            Id = Guid.NewGuid().ToString();
            Type = type;
            Price = price;
            Quantity = quantity;
        }

        //Only used for db to entity mapping
        public OrderItem(string id, string type, decimal price, int quantity)
        {
            Id =id;
            Type = type;
            Price = price;
            Quantity = quantity;
        }

        public string Id { get; private set; }
        public string Type { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set;  }

        public void IncreaseQuantityBy(int quantityToIncrease)
        {
            Quantity = Quantity + quantityToIncrease;
        }
    }
}