using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace Writer
{
    class Program
    {
        public static IContainer Container;

        public static void WriteTime()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IHourWriter>();
                writer.WriteHour();
            }
        }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().AsSelf().As<IOutput>();
            builder.RegisterType<SomeType>().AsSelf().As<IOutput>();
            builder.RegisterType<CurrentTimeDefaultColorWriter>().AsSelf().As<IHourWriter>();

            Container = builder.Build();

            WriteTime();

            Console.ReadLine();
        }
    }
}
