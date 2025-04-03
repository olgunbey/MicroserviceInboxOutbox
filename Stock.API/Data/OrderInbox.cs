namespace Stock.API.Data
{
    public class OrderInbox
    {
        public int Id { get; set; }
        public bool Processed { get; set; }
        public string Payload { get; set; }
    }

}
