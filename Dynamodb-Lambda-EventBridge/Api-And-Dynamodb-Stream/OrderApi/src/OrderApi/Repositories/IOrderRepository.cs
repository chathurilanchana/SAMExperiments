using Amazon.DynamoDBv2.DocumentModel;

namespace OrderApi.Repositories
{
    public interface IOrderRepository
    {
        public Task AddOrderAndOrderLines(List<Document> documents);

        public Task<Document?> GetlatestActiveOrder(string customerId, string status);

        public Task<List<Document>> GetOrderLines(string customerId , string orderId);
    }
}
