using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsExamples.IoTHub
{
    public static class IoTHubTwinEdits
    {

        /*
         *
         * GET function that will query Twins of devices
         * Can be used with parameters...
         *
         * Don't forget to insert the ConnectionString from YourIOTHub > Shared Access Points > iothubowner
         *
         * Requires Microsoft.Azure.Devices as NuGet package
         *
         */
        [FunctionName("GetDevices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "devices")] HttpRequest req,
            ILogger log)
        {
;
            //Create manager
            RegistryManager manager = RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IoTAdmin"));
            //Search query
            var devices = manager.CreateQuery("SELECT * FROM Devices");
            List<Twin> twins = new List<Twin>();

            while (devices.HasMoreResults)
            {
                var page = await devices.GetNextAsTwinAsync();
                foreach (var twin in page)
                {
                    twins.Add(twin);
                }
            }

            //Will return all twins
            return new OkObjectResult(twins);
        }
        /*
        *
        * GET function that will change a value in a twin of a device
        *
        * Don't forget to insert the ConnectionString from YourIOTHub > Shared Access Points > iothubowner
        *
        * Requires Microsoft.Azure.Devices as NuGet package
        *
        */
        [FunctionName("changeValue")]
        public static async Task<IActionResult> GetDevices(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "devices/{id}/{value}")] HttpRequest req,string id, int value,
            ILogger log)
        {

            //Insert connectionString from YourIOTHub > Shared Access Points > iothubowner
            RegistryManager manager = RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IoTAdmin"));

            //Get twin from id that is given in the Trigger
            var twin = await manager.GetTwinAsync(id);
            //Change value in the twin
            twin.Properties.Desired["VALUE_TO_CHANGE"] = value;
            //update the twin
            await manager.UpdateTwinAsync(id, twin, twin.ETag);

            return new OkObjectResult("");

            
        }
    }
}
