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
            Task.Factory.StartNew(() => GetStockMarket("ALL USD STOCKS", null));
            Task.Factory.StartNew(() => GetStockMarket("ALL STOCKS", null));
            Task.Factory.StartNew(() => GetStockMarket("ALL NYSE STOCKS", null));
            Task.Factory.StartNew(() => GetStockMarket("ALL EUR STUTTGART STOCKS", null));
            Console.WriteLine("Consumers started..");
            Console.ReadLine();
            FinishEvent.Set();
        }

        private static void GetStockMarket(string name, string key)
        {
            
        }
    }
}