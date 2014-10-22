using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Lab1Serialization
{
    class ProtocolBuffersPayloadSerializer : IPayloadSerializer
    {

        public byte[] Serialize(PayloadDto dto)
        {
            // tutaj wrzucamy kod serializacji
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, dto);
                return stream.ToArray();
            }
        }

        public PayloadDto Deserialize(byte[] data)
        {
            // tutaj wrzucamy kod deserializacji
            using (var stream = new MemoryStream(data))
            {
                return Serializer.Deserialize<PayloadDto>(stream);
            }
        }
    }
}
