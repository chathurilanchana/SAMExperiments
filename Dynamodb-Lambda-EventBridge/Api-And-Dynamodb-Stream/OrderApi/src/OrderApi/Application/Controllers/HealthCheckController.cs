using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<OrdersController> logger;

    public HealthCheckController(ILogger<OrdersController> logger)
    {
        this.logger = logger;
    }

    // GET api/books
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok();
    }
}
