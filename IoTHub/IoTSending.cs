using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsExamples.IoTHub
{
    public static class IoTSending
    {


    /*
    *
    * POST function that will send direct method OR message to the device in the queryparams
    *
    * Don't forget to insert the ConnectionString from YourIOTHub > Shared Access Points > iothubowner
    *
    * Requires Microsoft.Azure.Devices as NuGet package
    *
    */
    [FunctionName("IoTSending")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = "route/{deviceid}/thing")] HttpRequest req, string deviceid,
        ILogger log)
        {

            ServiceClient client = ServiceClient.CreateFromConnectionString(Environment.GetEnvironmentVariable("IoTHubAdmin"));


            //
            // SEND MESSAGE
            //

            // Getting body from POST request
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // sending contents of body to device
            Message message = new Message(Encoding.ASCII.GetBytes(requestBody));
            await client.SendAsync(deviceid, message);


            //
            // SEND DIRECT METHOD
            //

            //Change to method name
            CloudToDeviceMethod method = new CloudToDeviceMethod("reboot");
            //Include payload if needed
            method.SetPayloadJson("{'seconds':15}");
            //Invoke the method
            await client.InvokeDeviceMethodAsync(deviceid, method);


            return new OkObjectResult("");
        }
    }
}
