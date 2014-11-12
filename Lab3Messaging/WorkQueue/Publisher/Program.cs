using System;
using System.Text;
using System.Threading;
using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Publisher
{
    internal class Program
    {
        static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    var replyQueue = SetUpReplyChannel(channel);
                    
                    for (int i = 0; i < Const.MessageCount; i++)
                    {
                        string message = "Complex task #" + i;
                        
                        Console.WriteLine("Publishing #" + i);
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    }
                    Console.WriteLine("Done publishing");
                    Console.ReadLine();        
                }
            }
        }

        private static QueueDeclareOk SetUpReplyChannel(IModel channel)
        {
            return null;
        }

        private static void OnReply(BasicDeliverEventArgs args)
        {
            Console.WriteLine("Got reply {0} with corellation id: {1}", args.Body.GetString(), args.BasicProperties.CorrelationId);
        }
    }
}