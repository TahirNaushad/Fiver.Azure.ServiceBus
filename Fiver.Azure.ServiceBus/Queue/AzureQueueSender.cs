using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiver.Azure.ServiceBus.Queue
{
    public class AzureQueueSender<T> : IAzureQueueSender<T> where T : class
    {
        #region " Public "

        public AzureQueueSender(AzureQueueSettings settings)
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

        private AzureQueueSettings settings;
        private QueueClient client;

        private void Init()
        {
            client = new QueueClient(this.settings.ConnectionString, this.settings.QueueName);
        }

        #endregion
    }
}
