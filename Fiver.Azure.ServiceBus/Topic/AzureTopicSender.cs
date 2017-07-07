using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiver.Azure.ServiceBus.Topic
{
    public class AzureTopicSender<T> : IAzureTopicSender<T> where T : class
    {
        #region " Public "

        public AzureTopicSender(AzureTopicSettings settings)
        {
            this.settings = settings;
            Init();
        }

        public async Task SendAsync(T item)
        {
            await SendAsync(item, null);
        }

        public async Task SendAsync(T item, Dictionary<string, object> properties)
        {
            var json = JsonConvert.SerializeObject(item);
            var message = new Message(Encoding.UTF8.GetBytes(json));
            
            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    message.UserProperties.Add(prop.Key, prop.Value);
                }
            }

            await client.SendAsync(message);
        }

        #endregion

        #region " Private "

        private AzureTopicSettings settings;
        private TopicClient client;

        private void Init()
        {
            client = new TopicClient(this.settings.ConnectionString, this.settings.TopicName);
        }

        #endregion
    }
}
