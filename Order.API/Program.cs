using Microsoft.EntityFrameworkCore;
using Order.API.Context;
using Order.API.Entities;
using Shared.Events;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>(y => y.UseNpgsql("Host=localhost;Port=5432;Database=OrchestrationOrderAPI;Username=myuser;Password=mypassword;"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("order/createorder", async (OrderDbContext orderDbContext) =>
{
    var order = new Order.API.Entities.Order()
    {
        BuyerId = 21,
        OrderItems = new List<Order.API.Entities.OrderItem>()
        {
            {new Order.API.Entities.OrderItem{Id=1,Name="Nike Ayakkabý",Price=10,Count=1 }},
            {new Order.API.Entities.OrderItem{Id=2,Name="Puma Ayakkabý",Price=20,Count=3} }
        }
    };
    await orderDbContext.Order.AddAsync(order);

    await orderDbContext.OrderOutbox.AddAsync(new OrderOutbox()
    {
        ProcessedDate = null,
        Payload = JsonSerializer.Serialize(order),
        Type = new OrderCreatedEvent().GetType().Name
    });


});

app.Run();
