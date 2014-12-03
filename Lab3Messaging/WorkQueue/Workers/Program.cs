using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Workers
{
    internal class Program
    {
        private static readonly CountdownEvent AllMessagesReceived = new CountdownEvent(Const.MessageCount);

        private static void Main(string[] args)
        {
            var t1 = new Thread(FailingWorker);
            t1.Start();
            var t2 = new Thread(GoodWorker);
            t2.Start();
            Console.WriteLine("Consumers started....");
            Console.ReadLine();
        }

        private static void FailingWorker()
        {
            IModel channel;
            using (Connect(out channel))
            {
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(Const.QueueName, false, consumer);
                BasicDeliverEventArgs msg = consumer.Queue.Dequeue();
                Console.WriteLine("####### I got message: {0}", msg.Body.GetString());
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
            Console.WriteLine("######## I just died :(");
        }

        private static void GoodWorker()
        {
            IModel channel;
            using (Connect(out channel))
            {
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(Const.QueueName, false, consumer);
                for (int i = 0; i < Const.MessageCount; i++)
                {
                    BasicDeliverEventArgs arg = consumer.Queue.Dequeue();
                    Task.Factory.StartNew(() => GoodWorker_Process(arg, channel));
                }
                AllMessagesReceived.Wait();
            }
            Console.WriteLine("@@@@@@@ Finished :)");
        }

        private static void GoodWorker_Process(BasicDeliverEventArgs msg, IModel channel)
        {
            string task = msg.Body.GetString();
            Console.WriteLine("@@@@@@ I got message: {0}", task);
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            Console.WriteLine("@@@@@@ I finished processing message: {0}", task);
            SendReply(msg, channel);
            //confirm that message was processed
            channel.BasicAck(msg.DeliveryTag, false);

            AllMessagesReceived.Signal();
        }

        private static void SendReply(BasicDeliverEventArgs msg, IModel channel)
        {
            var props = channel.CreateBasicProperties();
            props.CorrelationId = msg.BasicProperties.CorrelationId;
            channel.BasicPublish(string.Empty, msg.BasicProperties.ReplyTo, props, "Hello".GetBytes());
        }

        private static CompositeDisposable Connect(out IModel channel)
        {
            var cd = new CompositeDisposable();
            var factory = new ConnectionFactory {HostName = "localhost"};
            IConnection conn = factory.CreateConnection();

            channel = conn.CreateModel();
            channel.ExchangeDeclare(Const.ExchangeName, ExchangeType.Direct);
            channel.QueueDeclare(Const.QueueName, false, false, true, null);
            channel.QueueBind(Const.QueueName, Const.ExchangeName, Const.RoutingKey);

            //wait for worker to finish
            channel.BasicQos(0, 1, false);

            cd.Add(conn);
            cd.Add(channel);
            return cd;
        }
    }
}