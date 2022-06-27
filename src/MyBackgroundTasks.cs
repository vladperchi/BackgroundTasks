using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundTasks
{
    public class MyBackgroundTasks : BackgroundService
    {
        private int executionCount = 0;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MyBackgroundTasks> _logger;

        public MyBackgroundTasks(IServiceProvider serviceProvider, ILogger<MyBackgroundTasks> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "MyBackgroundTasks: StartAsync {dateTime}", DateTime.Now);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "MyBackgroundTasks: StopAsync {dateTime}", DateTime.Now);
            return base.StopAsync(cancellationToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                using var scope = _serviceProvider.CreateScope();
                _logger.LogInformation(
                    "MyBackgroundTasks: ExcecuteAsync {dateTime}. Count: {Count}", DateTime.Now, executionCount);
                var scopedService = scope.ServiceProvider.GetRequiredService<IScopedService>();
                scopedService.Write();
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
