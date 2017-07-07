using Fiver.Azure.ServiceBus.Common;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Fiver.Azure.ServiceBus.Queue
{
    public class AzureQueueReceiver<T> : IAzureQueueReceiver<T> where T : class
    {
        #region " Public "

        public AzureQueueReceiver(AzureQueueSettings settings)
        {
            this.settings = settings;
            Init();
        }

        public void Receive(
            Func<T, MessageProcessResponse> onProcess,
            Action<Exception> onError,
            Action onWait)
        {
            var options = new MessageHandlerOptions
            {
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(1)
            };

            client.RegisterMessageHandler(
                async (message, token) =>
                {
                    try
                    {
                        // Get message
                        var data = Encoding.UTF8.GetString(message.Body);
                        T item = JsonConvert.DeserializeObject<T>(data);

                        // Process message
                        var result = onProcess(item);

                        if (result == MessageProcessResponse.Complete)
                            await client.CompleteAsync(message.SystemProperties.LockToken);
                        else if (result == MessageProcessResponse.Abandon)
                            await client.AbandonAsync(message.SystemProperties.LockToken);
                        else if (result == MessageProcessResponse.Dead)
                            await client.DeadLetterAsync(message.SystemProperties.LockToken);

                        // Wait for next message
                        onWait();
                    }
                    catch (Exception ex)
                    {
                        await client.DeadLetterAsync(message.SystemProperties.LockToken);
                        onError(ex);
                    }
                }, options);
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
