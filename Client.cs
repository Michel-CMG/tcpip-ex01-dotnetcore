using System;
using System.Net.Sockets;

namespace tcpip_ex01_dotnetcore
{
    class Client
    {
        private System.Net.IPEndPoint endpoint;

        public Client(System.Net.IPEndPoint endpoint)
        {
            this.endpoint = endpoint;
        }
        public Message Send(Message requestMsg)
        {
            using (var client = new System.Net.Sockets.TcpClient())
            {
                try
                {
                    // 01 Connect to server
                    client.Connect(this.endpoint);
                }
                catch (Exception)
                {
                    Console.WriteLine("Client says: Server is not running. Quit.");
                    var errorMessage = new Message(99, "Server is not running.");
                    return errorMessage;
                }

                using (var stream = client.GetStream())
                {
                    // 02 Send request and receive response
                    return this.SendReceive(requestMsg, stream);
                }
            }
        }

        private Message SendReceive(Message requestMsg, NetworkStream stream)
        {
            var utf8 = System.Text.Encoding.UTF8;
            
            // 01 Send request message
            using (var bWriter = new System.IO.BinaryWriter(stream, utf8, true))
            {
                var requestBytes = this.MessageToBytes(requestMsg);
                bWriter.Write(requestBytes.Length);
                bWriter.Write(requestBytes);
            }
            Console.WriteLine($"Client says: send request '{requestMsg}'");

            // 02 Get response message
            using (var bReader = new System.IO.BinaryReader(stream, utf8))
            {
                var length = bReader.ReadInt32();
                var bytes = bReader.ReadBytes(length);
                var responseMsg = this.BytesToMessage(bytes);
                Console.WriteLine($"Client says: receive response '{responseMsg}'");
                return responseMsg;
            }
        }

        private Byte[] MessageToBytes(Message message)
        {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var stream = new System.IO.MemoryStream())
            {
                formatter.Serialize(stream, message);
                return stream.ToArray();
            }
        }

        private Message BytesToMessage(Byte[] bytes)
        {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var stream = new System.IO.MemoryStream(bytes))
            {
                return (Message)formatter.Deserialize(stream);
            }
        }
    }
}
