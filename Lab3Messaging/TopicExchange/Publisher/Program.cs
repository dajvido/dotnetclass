using System;
using RabbitMQ.Client;

namespace Publisher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hit enter to publish stocks");
            Console.ReadLine();

            string nyseKey = "stock.usd.nyse";
            string nyseMessage = "USD/NYSE stock data";

            string swbKey = "stock.eur.swb";
            string swbMessage = "EUR/SWB stock data";

            Console.WriteLine("All data published");
            Console.ReadLine();
        }
    }
}