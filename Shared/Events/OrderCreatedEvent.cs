namespace Shared.Events
{
    public class OrderCreatedEvent
    {
        public int BuyerId { get; set; }
        public int MyProperty { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalPrice => OrderItems.Sum(y => y.Count * y.Price);
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
