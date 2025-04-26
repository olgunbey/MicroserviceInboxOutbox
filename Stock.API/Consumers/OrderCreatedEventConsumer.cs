using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Stock.API.Data;
using System.Text.Json;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {

        private readonly StockDbContext _stockDbContext;
        public OrderCreatedEventConsumer(StockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {

            var result = await _stockDbContext.OrderInbox.Where(y => y.IdempotentToken == context.Message.IdempotentToken).AnyAsync();

            if (!result)
            {
                _stockDbContext.OrderInbox.Add(new OrderInbox()
                {
                    Processed = false,
                    Payload = JsonSerializer.Serialize(context.Message),
                    IdempotentToken = context.Message.IdempotentToken

                });
                await _stockDbContext.SaveChangesAsync();
            }



            List<OrderInbox> orderInboxes = await _stockDbContext.OrderInbox
                .Where(y => y.Processed == false)
                .ToListAsync();

            foreach (var orderInbox in orderInboxes)
            {
                //burada orderitems'lerin stock kontrolunu yapabilirsin!!!!
                OrderCreatedEvent orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderInbox.Payload);
                orderInbox.Processed = true;
                await _stockDbContext.SaveChangesAsync();
            }

        }
    }
}
