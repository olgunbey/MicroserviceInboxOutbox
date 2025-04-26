using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Context;
using Shared.Events;
using System.Text.Json;

namespace Order.API.Job
{
    public class OrderOutboxJob
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderOutboxJob(OrderDbContext orderDbContext, IPublishEndpoint publishEndpoint)
        {
            _orderDbContext = orderDbContext;
            _publishEndpoint = publishEndpoint;
        }
        public async Task ExecuteJob()
        {
            var orderOutboxUnProcessed = _orderDbContext.OrderOutbox.Where(y => y.ProcessedDate == null);

            var data = await orderOutboxUnProcessed.ToListAsync();

            await orderOutboxUnProcessed.ExecuteUpdateAsync(setter =>
             setter.SetProperty(y => y.ProcessedDate, DateTime.UtcNow));
            foreach (var orderOutbox in data)
            {
                if (orderOutbox.Type == nameof(OrderCreatedEvent))
                {
                    OrderCreatedEvent orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderOutbox.Payload)!;
                    orderCreatedEvent.IdempotentToken = orderOutbox.IdempotentToken;
                    await _publishEndpoint.Publish(orderCreatedEvent);

                }
            }
        }
    }
}
