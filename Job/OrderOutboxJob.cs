namespace Job
{
    public class OrderOutboxJob
    {
        public OrderOutboxJob(OrderDbContext order)
        {

        }
        public void ExecuteJob()
        {
            Console.WriteLine(DateTime.UtcNow);
        }
    }
}
