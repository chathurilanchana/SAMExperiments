using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application;
using OrderApi.Application.Models;

namespace OrderApi.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> logger;
    private readonly IOrderService _orderService;

    public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
    {
        this.logger = logger;
        _orderService = orderService;
    }


    // POST api/orders
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateOrderRequest request)
    {
        await _orderService.HandleAsync<CreateOrderRequest>(request);

        return Ok();
    }

    //get api/orders/{customerId}/active
    [HttpGet]
    public async Task<ActionResult<GetOrderResponse>> GetLatestActiveOrder(string customerId)
    {
        var result = await _orderService.HandleAsync<string, GetOrderResponse>(customerId);

        return Ok(result);
    }
}
