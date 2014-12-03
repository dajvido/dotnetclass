using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Writer
{
    public class SomeType : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine("Bla bla bla bla...");
        }
    }
}
