using System;

namespace Fiver.Azure.ServiceBus.Topic
{
    public class AzureTopicSettings
    {
        public AzureTopicSettings(string connectionString, string topicName)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            if (string.IsNullOrEmpty(topicName))
                throw new ArgumentNullException("topicName");

            this.ConnectionString = connectionString;
            this.TopicName = topicName;
        }

        public string ConnectionString { get; }
        public string TopicName { get; }
    }
}
