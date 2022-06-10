using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace OrderApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private const string TableName = "Orders";
        private readonly Table _table;

        public OrderRepository(IAmazonDynamoDB amazonDynamoDbClient)
        {
            _table = Table.LoadTable(amazonDynamoDbClient, TableName);
        }

        public async Task AddOrderAndOrderLines(List<Document> documents)
        {
            var batchWrite = _table.CreateBatchWrite();

            foreach (var document in documents)
            {
                batchWrite.AddDocumentToPut(document);
            }

           await batchWrite.ExecuteAsync();
        }

        public async Task<Document?> GetlatestActiveOrder(string customerId, string status)
        {
            var filter = new QueryFilter("GSI1-PK", QueryOperator.Equal, $"CUSTOMER#{customerId}");
            filter.AddCondition("GSI1-SK", QueryOperator.BeginsWith, $"STATUS#{status}");

            var queryOperationConfig = new QueryOperationConfig
            {
                Filter = filter,
                IndexName = "GSI1-index",
                BackwardSearch = true,
                Limit = 1
            };

            var documents = await _table.Query(queryOperationConfig).GetNextSetAsync(); // Get next set returns the specified set of records

            return documents.FirstOrDefault();

        }

        /* To do a paged query use below(eg: Retrieving all data in a single query cause throttling)
        var search = _table.Query(queryOperationConfig);

        var documents = new List<Document>();

            do {
                List<Document> nextSet = await search.GetNextSetAsync();
        documents.AddRange(nextSet);

            } while (!search.IsDone);

        */

        public async Task<List<Document>> GetOrderLines(string customerId , string orderId)
        {
            var filter = new QueryFilter("PK", QueryOperator.Equal, $"CUSTOMER#{customerId}");
            filter.AddCondition("SK", QueryOperator.BeginsWith, $"ORDER#{orderId}#OrderLine#");

            var queryOperationConfig = new QueryOperationConfig
            {
                Filter = filter,
            };

            var result = await _table.Query(queryOperationConfig).GetRemainingAsync(); // Get remaining returns all the results, limits not applicable to it

            return result;
        }

    }
}
