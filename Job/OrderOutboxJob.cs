namespace Job
{
    public class OrderOutboxJob
    {
        public void ExecuteJob()
        {
            Console.WriteLine(DateTime.UtcNow);
        }
    }
}
