using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsExamples.Queues
{
    public static class Queues
    {
        /*
         *
         * Will send data to the queue
         *
         */
        private static async Task PutItemInQueue(ExampleRequest request)
        {
            //connect to queue
            string connection = Environment.GetEnvironmentVariable("ConnectionStringStorage");
            //Enter right Queue name
            QueueClient queueClient = new QueueClient(connection, "registrations");

            await queueClient.CreateIfNotExistsAsync(); // Creates queue if not exists

            string json = JsonConvert.SerializeObject(request); //has to be converted to json then to Bytes
            var valueBytes = Encoding.UTF8.GetBytes(json); // then to bytes

            // put the message in the queue
            await queueClient.SendMessageAsync(Convert.ToBase64String(valueBytes)); //Has to be base64 encoded
        }


        /*
         *
         * Function that will be triggered when item enters a queue
         * Required Azure.Storage.Queues as NuGet package
         *
         */
        [FunctionName("QueueTrigger")]
        public static void QueueTrigger([QueueTrigger("queue_name", Connection = "change_me")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            //Converting data to required object (ExampleRequest)
            //Only works when sent data is JSON
            ExampleRequest exampleRequest = JsonConvert.DeserializeObject<ExampleRequest>(myQueueItem);
        }
    }
}

