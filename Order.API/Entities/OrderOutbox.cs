namespace Order.API.Entities
{
    public class OrderOutbox
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}
