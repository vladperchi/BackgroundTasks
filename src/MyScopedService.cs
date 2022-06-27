using System;
using Microsoft.Extensions.Logging;

namespace BackgroundTasks
{
    public class MyScopedService : IScopedService
    {
        private readonly ILogger<MyScopedService> _logger;

        public MyScopedService(ILogger<MyScopedService> logger)
        {
            _logger = logger;
        }

        public void Write()
        {
            var guid = Guid.NewGuid().ToString();
            _logger.LogInformation("MyScopedService {Id} - {dateTime}", guid, DateTime.Now);
        }
    }

    public interface IScopedService
    {
        void Write();
    }
}
