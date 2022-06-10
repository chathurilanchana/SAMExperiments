using System.Text.Json;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using OrderApi.Application;
using OrderApi.Application.Handlers;
using OrderApi.Application.Models;
using OrderApi.Infrastructure;
using OrderApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Logger
builder.Logging
        .ClearProviders()
        .AddJsonConsole();


// Add services to the container.
builder.Services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var region = Environment.GetEnvironmentVariable("AWS_REGION") ?? RegionEndpoint.EUNorth1.SystemName;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env.Equals("Local"))
{
    builder.Services.AddDefaultAWSOptions(new Amazon.Extensions.NETCore.Setup.AWSOptions
    {
        Region = Amazon.RegionEndpoint.EUNorth1,
        Profile = "chathuri"
    });
    builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
    {
        var credentials = new BasicAWSCredentials("<AccessKey>", "<SecretKey>");
        return new AmazonDynamoDBClient(credentials);
    });
}
else
{
    builder.Services.AddAWSService<IAmazonDynamoDB>();
    builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
}
builder.Services
        .AddTransient<IDynamoDBContext, DynamoDBContext>()
        .AddTransient<IOrderRepository, OrderRepository>()
        .AddTransient<ICommandHandler<CreateOrderRequest>, CreateOrderHandler>()
           .AddTransient<IQueryHandler<string, GetOrderResponse>, GetLatestActiveOrderHandler>()
        .AddTransient<IOrderService, OrderService>();


var app = builder.Build();

if (env.Equals("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.Run();
