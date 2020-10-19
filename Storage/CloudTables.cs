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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /*
         *
         * Get function that will get all or specified data from table storage
         *
         */
        [FunctionName("GETWithAzureStorage")]
        public static async Task<IActionResult> etRegistrationsV2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "path/that/i/need/{age}")] HttpRequest req,
            int age,
            ILogger log)
        {
            /*
            *
            * Dont forget to add your ConnectionString to local.settings.json and changee the name under here
            * Can be found in the Azure portal
            *
            */
            string connection = Environment.GetEnvironmentVariable("CHANGE_ME");

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connection);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable cloudTable = cloudTableClient.GetTableReference("registrations");

            //Getting all rows
            TableQuery<ExampleRequestEntity> rangeQueryAll = new TableQuery<ExampleRequestEntity>();

            //Get all rows with a specific value in a column
            // bit like WHERE clause in SQL
            TableQuery<ExampleRequestEntity> rangeQuery = new TableQuery<ExampleRequestEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, age.ToString()));
            var queryresult = await cloudTable.ExecuteQuerySegmentedAsync<ExampleRequestEntity>(rangeQuery, null);

            //Loop each result and parse it to required type, this case ExampleRequest
            List<ExampleRequest> registrations = new List<ExampleRequest>();
            foreach (var reg in queryresult.Results)
            {

                registrations.Add(new ExampleRequest()
                {
                    name = reg.name,
                    address = reg.address,
                    age = reg.age
                });

            }

            //return the list
            return new OkObjectResult(registrations);
        }

    }
}
