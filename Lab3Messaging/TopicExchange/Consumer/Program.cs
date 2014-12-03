using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    internal class Program
    {
        private static readonly ManualResetEvent FinishEvent = new ManualResetEvent(false);

        private static void Main(string[] args)
        {
            Task.Factory.StartNew(() => GetStockMarket("ALL USD STOCKS", "stock.*.nyse"));
            Task.Factory.StartNew(() => GetStockMarket("ALL STOCKS", "stock.#"));
            Task.Factory.StartNew(() => GetStockMarket("ALL NYSE STOCKS", "stock.*.nyse"));
            Task.Factory.StartNew(() => GetStockMarket("ALL EUR STUTTGART STOCKS", "stock.eur.swb"));
            Console.WriteLine("Consumers started..");
            Console.ReadLine();
            FinishEvent.Set();
        }

        private static void GetStockMarket(string name, string key)
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel model = conn.CreateModel())
                {
                    model.ExchangeDeclare(Const.ExchangeName, ExchangeType.Topic);
                    QueueDeclareOk queue = model.QueueDeclare();
                    var consumer = new EventingBasicConsumer(model);
                    consumer.Received += (_, msg) => Console.WriteLine("Consumer {0} received message {1}", name, msg.Body.GetString());
                    model.QueueBind(queue.QueueName, Const.ExchangeName, key);
                    model.BasicConsume(queue.QueueName, false, consumer);
                    Console.WriteLine("Consumer {0} is waiting for message matching topic {1}", name, key);
                    FinishEvent.WaitOne();
                }
            }
        }
    }
}