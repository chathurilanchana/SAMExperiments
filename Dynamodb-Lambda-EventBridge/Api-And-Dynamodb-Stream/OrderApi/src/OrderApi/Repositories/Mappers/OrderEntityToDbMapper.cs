using Amazon.DynamoDBv2.DocumentModel;
using OrderApi.Domain.Entities;

namespace OrderApi.Repositories.Mappers
{
    public class OrderEntityToDbMapper
    {
        //TODO: Refactor this to be more object oriented
        public static List<Document> MapOrderEntityToDynamoDBDocuments(Order order)
        {
            var documents = new List<Document>();
            var orderDocument = new Document();
            orderDocument["PK"] = $"CUSTOMER#{order.CustomerId}";
            orderDocument["SK"] = $"ORDER#{order.Id}"; // find order if you already have orderId
            orderDocument["GSI1-PK"] = $"CUSTOMER#{order.CustomerId}";
            orderDocument["GSI1-SK"] = $"STATUS#{order.Status.ToString()}#TIMESTAMP#{order.CreatedDate}"; // GSI1 lookup to find the latest order
            orderDocument["Id"] = order.Id;
            orderDocument["CustomerId"] = order.CustomerId;
            orderDocument["CreatedOn"] = order.CreatedDate;
            orderDocument["status"] = order.Status.ToString();
            documents.Add(orderDocument);

            foreach (var item in order.OrderItems)
            {
                var document = new Document();
                document["PK"] = $"CUSTOMER#{order.CustomerId}";
                document["SK"] = $"ORDER#{order.Id}#OrderLine#{item.Id}";
                document["Id"] = order.Id;
                document["Amount"] = item.Price;
                document["Type"] = item.Type;
                document["Quantity"] = item.Quantity;
                documents.Add(document);
            }


            return documents;
        }

        //todo: Throw exception if none of the properties are as expected
        public static Order MapOrderAndOrderLineDocumentsToOrderEntity(Document order, List<Document> orderLines)
        {
            var orderItems = new List<OrderItem>();
            foreach (var document in orderLines)
            {
                var item = new OrderItem(document["Id"], document["Type"], decimal.Parse(document["Amount"]), int.Parse(document["Quantity"]));
                orderItems.Add(item);
            }

            var status = (OrderStatus) Enum.Parse(typeof(OrderStatus), order["status"]);
            return new Order(order["CustomerId"], order["Id"], DateTime.Parse(order["CreatedOn"]), orderItems, status);
        }

        public static string? GetOrderId(Document document)
        {
            if(document == null)
            {
                return null;
            }

            if (document.ContainsKey("Id"))
            {
                return document["Id"];
            }

            return null;
        }
    }
}
