using System;
using System.Collections.Generic;
using System.Text;

namespace Fiver.Azure.ServiceBus.Queue
{
    public class AzureQueueSettings
    {
        public AzureQueueSettings(string connectionString, string queueName)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentNullException("queueName");

            this.ConnectionString = connectionString;
            this.QueueName = queueName;
        }

        public string ConnectionString { get; }
        public string QueueName { get; }
    }
}
