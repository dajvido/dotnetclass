using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Writer
{
    public class CurrentTimeDefaultColorWriter : IHourWriter
    {
        private IOutput _output;

        public CurrentTimeDefaultColorWriter(IOutput output)
        {
            this._output = output;
        }
        public void WriteHour()
        {
            this._output.Write(DateTime.Now.ToShortTimeString());
        }
    }
}
