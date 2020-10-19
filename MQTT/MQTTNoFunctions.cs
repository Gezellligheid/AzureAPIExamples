using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace FunctionsExamples
{
    public class MQTTNoFunctions
    {


        /*
         *
         * Connecting as a client
         * See this as the start of your application
         *
         */
        public MQTTNoFunctions()
        {

            //Connecting
            string brokerHost = "0.1.2.3";
            MqttClient client = new MqttClient(brokerHost);
            client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);


            //sending messages
            string ShitIWantToSend = "ThisIsAMessage";
            client.Publish("/home/temperature", Encoding.UTF8.GetBytes(ShitIWantToSend),MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,false);


            /*
             *
             * Listening to messages
             * Subscribing to an endpoint
             *
             * Note:
             * Can have multiple endpoints
             *
             */
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.Subscribe(new string[] {"/home/temperatur"}, new byte[] {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE});
        }


        //EventHandler
        private void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {

            byte[] data = e.Message;

            //do stuff with data
            System.Console.WriteLine(data);
            
        }
    }
}
