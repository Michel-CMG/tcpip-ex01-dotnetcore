using System.Runtime.Serialization.Formatters.Binary;

namespace tcpip_ex01_dotnetcore
{
    [System.Serializable]
    class Message
    {
        public int id { get; set; }
        public string content { get; set; }

        public Message(int id, string content)
        {
            this.id = id;
            this.content = content;
        }

        public Message() {}

        public override string ToString()
        {
            string idToPrint = this.id.ToString("D2");
            return $"{{ id:{idToPrint}, content:\"{this.content}\" }}";
        }

        public byte[] ToBytes()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new System.IO.MemoryStream())
            {
                formatter.Serialize(stream, this);
                return stream.ToArray();
            }
        }

        public static Message BytesToMessage(byte[] bytes)
        {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var stream = new System.IO.MemoryStream(bytes))
            {
                return (Message)formatter.Deserialize(stream);
            }
        }
    }
}
