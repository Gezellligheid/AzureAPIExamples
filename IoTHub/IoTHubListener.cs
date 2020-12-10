using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace FunctionsExamples.IoTHub
{
    public static class IoTHubListener
    {
        /*
         *
         * IotHub Trigger that will fire whenever a message is sent to the hub
         * Don't forget to add the connectionstring to local.settings.json
         *
         */
        [FunctionName("IoTHubListener")]
        public static void Run([IoTHubTrigger("messages/events", Connection = "IotConnection")]EventData message, ILogger log)
        {
            log.LogInformation($"message: {Encoding.UTF8.GetString(message.Body.Array)}");
        }
    }
}