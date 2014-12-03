using System;
using Common;
using RabbitMQ.Client;

namespace Publisher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            Console.WriteLine("Hit enter to publish stocks");
            Console.ReadLine();
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel model = conn.CreateModel())
                {
                    model.ExchangeDeclare(Const.ExchangeName, ExchangeType.Topic);
                    string nyseKey = "stock.usd.nyse";
                    string nyseMessage = "USD/NYSE stock data";
                    model.BasicPublish(Const.ExchangeName, nyseKey, model.CreateBasicProperties(), nyseMessage.GetBytes());

                    string swbKey = "stock.eur.swb";
                    string swbMessage = "EUR/SWB stock data";
                    model.BasicPublish(Const.ExchangeName, swbKey, model.CreateBasicProperties(), swbMessage.GetBytes());
                }
            }
            Console.WriteLine("All data published");
            Console.ReadLine();
        }
    }
}