using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CaseOnline.Azure.WebJobs.Extensions.Mqtt;
using CaseOnline.Azure.WebJobs.Extensions.Mqtt.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsExamples
{
    public static class MQTTFunctions
    {



        /*
         *
         * Will execute code when a message is sent to the broker to a specific subscription topic
         * Will resend something to broker at path /out
         *
         */
        [FunctionName("MQTTFunctionsWithResend")]
        public static void MQTTFunctionsWithResend([MqttTrigger("/home/temperature")] IMqttMessage message, [Mqtt] out IMqttMessage outMessage ,ILogger log)
        {
            //getting the message that has been sent   
            var body = message.GetMessage();
            var bodyMessageg = Encoding.UTF8.GetString(body);

            //sending back a message to a topic
            var newMessage = $"{message} from server";
            outMessage = new MqttMessage("/out", Encoding.ASCII.GetBytes(newMessage),
                MqttQualityOfServiceLevel.AtLeastOnce, true);

            /*
             *
             * Do stuff with the message
             * for example Post to database, etc...
             *
             */

        }


        /*
         *
         * Will execute code when a message is sent to the broker to a specific subscription topic
         *
         */
        [FunctionName("MQTTFunctionsNoResend")]
        public static void MQTTFunctionsNoResend([MqttTrigger("/home/temperature")] IMqttMessage message, ILogger log)
        {
            //getting the message that has been sent   
            var body = message.GetMessage();
            var bodyMessageg = Encoding.UTF8.GetString(body);
            

            /*
             *
             * Do stuff with the message
             * for example Post to database, etc...
             *
             */

        }
    }
}
