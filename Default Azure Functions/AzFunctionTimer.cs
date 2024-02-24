using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Default_Azure_Functions
{
    public class AzFunctionTimer
    {
        private readonly ILogger _logger;

        public AzFunctionTimer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AzFunctionTimer>();
        }

        [Function("AzFunctionTimer")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
