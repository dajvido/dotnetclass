using System.IO;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Serialization
{
    class XmlPayloadSerializer : IPayloadSerializer
    {
        public byte[] Serialize(PayloadDto dto)
        {
            //var serializer = new XmlSerializer();
            //TextWriter textWriter = new StreamWriter(@"C:\Users\Dawid\Documents\GitHub\dotnetclass\Lab1Serialization\Lab1Serialization\xmlserialize.xml");
            ////serializer.SerializeToString();
            
            //textWriter.Close();
        }

        public PayloadDto Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
