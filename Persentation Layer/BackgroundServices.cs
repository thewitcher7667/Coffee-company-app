namespace Persentation_Layer
{
    public class BackgroundServices : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("hihihihihiiiiiiiiiiiiiiiiiiiiiiiiiii");
                await Task.Delay(1000, stoppingToken);    
            };
        }
    }
}
