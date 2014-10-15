using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Serialization
{
    class JSONPayloadSerializer : IPayloadSerializer
    {
        public byte[] Serialize(PayloadDto dto)
        {
            using (var stream = new MemoryStream())
            {
                JsonSerializer.SerializeToStream(dto, stream);
                return stream.GetBuffer();
            }
        }

        public PayloadDto Deserialize(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return JsonSerializer.DeserializeFromStream<PayloadDto>(stream);
            }
        }
    }
}
