using Fiver.Azure.ServiceBus.Common;
using Fiver.Azure.ServiceBus.Queue;
using Fiver.Azure.ServiceBus.Subscription;
using Fiver.Azure.ServiceBus.Topic;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Fiver.Azure.ServiceBus.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Queue
                //Queue_Send().Wait();
                //Queue_Receive();

                // Topic
                //Topic_Send().Wait();

                // Subscription
                //Subscription_Receive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        #region " Queue "

        private static async Task Queue_Send()
        {
            var config = GetConfig();

            var settings = new AzureQueueSettings(
                connectionString: config["ServiceBus_ConnectionString"],
                queueName: config["ServiceBus_QueueName"]);

            var message = new Message { Text = $"Hello Queue at {DateTime.Now.ToString()}" };

            IAzureQueueSender<Message> sender = new AzureQueueSender<Message>(settings);
            await sender.SendAsync(message);

            Console.WriteLine("Sent");
        }

        private static void Queue_Receive()
        {
            var config = GetConfig();

            var settings = new AzureQueueSettings(
                connectionString: config["ServiceBus_ConnectionString"],
                queueName: config["ServiceBus_QueueName"]);

            IAzureQueueReceiver<Message> receiver = new AzureQueueReceiver<Message>(settings);
            receiver.Receive(
                message =>
                {
                    throw new ApplicationException("Oops!");
                    Console.WriteLine(message.Text);
                    return MessageProcessResponse.Complete;
                },
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Waiting..."));
        }

        #endregion

        #region " Topic / Subscription "

        private static async Task Topic_Send()
        {
            var config = GetConfig();

            var settings = new AzureTopicSettings(
                connectionString: config["ServiceBus_ConnectionString"],
                topicName: config["ServiceBus_TopicName"]);

            var message = new Message { Text = $"Hello Topic at {DateTime.Now.ToString()}" };

            IAzureTopicSender<Message> sender = new AzureTopicSender<Message>(settings);
            await sender.SendAsync(message);

            Console.WriteLine("Sent");
        }
        
        private static void Subscription_Receive()
        {
            var config = GetConfig();

            var settings = new AzureSubscriptionSettings(
                connectionString: config["ServiceBus_ConnectionString"],
                topicName: config["ServiceBus_TopicName"],
                subscriptionName: config["ServiceBus_SubscriptionName"]);

            IAzureSubscriptionReceiver<Message> receiver = new AzureSubscriptionReceiver<Message>(settings);
            receiver.Receive(
                message =>
                {
                    Console.WriteLine(message.Text);
                    return MessageProcessResponse.Complete;
                },
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Waiting..."));
        }

        #endregion

        #region " Configuration "

        private static IConfigurationRoot GetConfig()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true)
                                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                                .AddEnvironmentVariables();

            return builder.Build();
        }

        #endregion
    }
}