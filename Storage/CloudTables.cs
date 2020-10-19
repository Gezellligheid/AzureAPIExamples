using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FunctionsExamples.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsExamples
{
    class CloudTables
    {


        /*
         *
         * Post to azure table storage
         * Requires Microsoft.Azure.Cosmos.Table as NuGet package
         *
         * Requires a TableEntity to send data to the table.
         * See Models/ExampleEntity
         *
         */
        [FunctionName("POSTWithAzureStorage")]
        public static async Task<IActionResult> PostAzureStorage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "postroute")] HttpRequest req,
            ILogger log)
        {

            //Converting body to ExampleRequest
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ExampleRequest exampleRequest = JsonConvert.DeserializeObject<ExampleRequest>(requestBody);

            //Converting ExampleRequest to ExampleRequestEntity
            ExampleRequestEntity exampleRequesEntity = new ExampleRequestEntity()
            {

                name = exampleRequest.name,
                address = exampleRequest.address,
                age = exampleRequest.age

            };


            /*
            *
            * Dont forget to add your ConnectionString to local.settings.json and changee the name under here
            * Can be found in the Azure portal
            *
            */
            string connenction = Environment.GetEnvironmentVariable("CHANGE_ME");

            //Azure Table storage shit
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connenction);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable cloudTable = cloudTableClient.GetTableReference("registrations");
            await cloudTable.CreateIfNotExistsAsync(); // Creates table if not exists
            TableOperation insertOperation = TableOperation.Insert(exampleRequesEntity);
            await cloudTable.ExecuteAsync(insertOperation);

            return new OkObjectResult(exampleRequesEntity);
        }

    }
}
