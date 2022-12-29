using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;

namespace somiodApp.Utils
{
    public class MessageBroker_utils
    {

        static MqttClient mClient;
       
        public static void connectPublish(string type,string endpoint, string channel , string message)
        {
            switch (type)
            {
                case "mqtt":
                    connectMQTT(endpoint);
                    publishMQTT(channel, message);                   
                    break;
                default:
                    break;
            }
        }
        
        public static bool connectMQTT(string endpoint)
        {
            mClient = new MqttClient(endpoint);
            mClient.Connect(Guid.NewGuid().ToString());

            if (!mClient.IsConnected)
                return false;

            return true;
        }
  
        public static void publishMQTT(string channel,string message)
        {
            if (mClient.IsConnected)
            {
                mClient.Publish(channel, Encoding.UTF8.GetBytes(message));
            }
                     
        }
    }
}