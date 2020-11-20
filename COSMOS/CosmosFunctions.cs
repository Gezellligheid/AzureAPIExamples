using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsExamples.COSMOS
{
    public static class CosmosFunctions
    {

        /*
         *
         *
         *  Will create an item in a Cosmos Database
         *
         *  Do not forget your connectionstring in local.settings.json
         *  Do not forget to install Microsoft.Azure.Cosmos as a NuGet package
         *
         */
        [FunctionName("AddItemToCosmos")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            //Read json from post
            //Can be message from IoTHub etc...
            string json = await new StreamReader(req.Body).ReadToEndAsync();

            //Cast json to the required object
            ExampleRequest request = JsonConvert.DeserializeObject<ExampleRequest>(json);
            //MANDATORY property has to be created called id
            request.id = Guid.NewGuid().ToString();

            //Create Cosmos client
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;
            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container in a database
            Container container = client.GetContainer("NameOfDatabase", "NameOfContainer");
            //Get the response
            ItemResponse<ExampleRequest> response =
                await container.CreateItemAsync(request, new PartitionKey(request.name));

            return new OkObjectResult("");
        }
    }
}
