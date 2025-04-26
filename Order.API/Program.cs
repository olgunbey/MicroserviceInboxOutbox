using Hangfire;
using Hangfire.MemoryStorage;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Context;
using Order.API.Entities;
using Order.API.Job;
using Shared.Events;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(y => y.UseMemoryStorage());
builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetSection("AmqpConf")["Host"], config =>
        {
            config.Username(builder.Configuration.GetSection("AmqpConf")["Username"]);
            config.Password(builder.Configuration.GetSection("AmqpConf")["Password"]);

        });

    });
});

builder.Services.AddHangfireServer();
builder.Services.AddDbContext<OrderDbContext>(y => y.UseNpgsql(builder.Configuration.GetConnectionString("order")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard();


RecurringJob.AddOrUpdate<OrderOutboxJob>("orderOutbox", y => y.ExecuteJob(), "*/15 * * * * *");

app.MapPost("order/createorder", async (OrderDbContext orderDbContext) =>
{
    var orderItems = new List<Order.API.Entities.OrderItem>()
    {
        {new Order.API.Entities.OrderItem{Id=1,Name="Nike Ayakkabý",Price=10,Count=1 }},
        {new Order.API.Entities.OrderItem{Id=2,Name="Puma Ayakkabý",Price=20,Count=3} }
    };
    var order = new Order.API.Entities.Order()
    {
        BuyerId = 21,
        OrderItems = orderItems,
        TotalPrice = orderItems.Sum(y => y.Price * y.Count)
    };
    await orderDbContext.Order.AddAsync(order);

    await orderDbContext.OrderOutbox.AddAsync(new OrderOutbox()
    {
        ProcessedDate = null,
        Payload = JsonSerializer.Serialize(order),
        Type = new OrderCreatedEvent().GetType().Name,
        IdempotentToken = Guid.NewGuid(),
    });

    await orderDbContext.SaveChangesAsync();

});



app.Run();
