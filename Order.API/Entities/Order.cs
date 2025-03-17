namespace Order.API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public int BuyerId { get; set; }
    }
}
