namespace Shared.Events
{
    public class OrderCreatedEvent
    {
        public int Id { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid IdempotentToken { get; set; }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
