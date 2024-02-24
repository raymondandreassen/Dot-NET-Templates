using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace My_Azure_Functions_App.AzFunctions
{
    public class TimerFunction2
    {
        private readonly ILogger _logger;

        public TimerFunction2(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TimerFunction2>();
        }

        [Function("TimerFunction2")]
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
