using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ToDoApp.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITodoTaskService _todoTaskService;

        public Worker(ILogger<Worker> logger, ITodoTaskService todoTaskService)
        {
            _logger = logger;
            _todoTaskService = todoTaskService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker çalışıyor: {time}", DateTimeOffset.Now);

                var tasks = await _todoTaskService.GetAllAsync();
                var overdueTasks = tasks.Where(t => !t.IsCompleted && t.DueDate < DateTime.UtcNow);

                foreach (var task in overdueTasks)
                {
                    _logger.LogWarning("🔔 Gecikmiş görev: {title} - {duedate}", task.Title, task.DueDate);
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
