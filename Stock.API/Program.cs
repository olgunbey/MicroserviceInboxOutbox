using MassTransit;
using Microsoft.EntityFrameworkCore;
using Stock.API.Consumers;
using Stock.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<StockDbContext>(y => y.UseNpgsql(builder.Configuration.GetConnectionString("stock")));
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OrderCreatedEventConsumer>();

    configurator.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetSection("AmqpConf")["Host"], config =>
        {
            config.Username(builder.Configuration.GetSection("AmqpConf")["Username"]);
            config.Password(builder.Configuration.GetSection("AmqpConf")["Password"]);

        });
        config.ReceiveEndpoint("order-created-event", conf => conf.ConfigureConsumer<OrderCreatedEventConsumer>(context));

    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
