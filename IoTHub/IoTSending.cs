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

            //Replace "Payload" with the message that has to be sent
            Message message = new Message(Encoding.ASCII.GetBytes("Payload"));
            await client.SendAsync(deviceid, message);


            //
            // Send direct method
            //

            //Change to method name
            CloudToDeviceMethod method = new CloudToDeviceMethod("reboot");
            //Include payload if needed
            method.SetPayloadJson("{'seconds':15}");
            await client.InvokeDeviceMethodAsync(deviceid, method);


            return new OkObjectResult("");
        }
    }
}
