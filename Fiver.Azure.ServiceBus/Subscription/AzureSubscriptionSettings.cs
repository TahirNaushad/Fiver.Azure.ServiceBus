using System;

namespace Fiver.Azure.ServiceBus.Subscription
{
    public class AzureSubscriptionSettings
    {
        public AzureSubscriptionSettings(string connectionString, string topicName, string subscriptionName)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            if (string.IsNullOrEmpty(topicName))
                throw new ArgumentNullException("topicName");

            if (string.IsNullOrEmpty(subscriptionName))
                throw new ArgumentNullException("subscriptionName");

            this.ConnectionString = connectionString;
            this.TopicName = topicName;
            this.SubscriptionName = subscriptionName;
        }

        public string ConnectionString { get; }
        public string TopicName { get; }
        public string SubscriptionName { get; }
    }
}
