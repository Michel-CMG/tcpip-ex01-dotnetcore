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
            
            // 02-01 Send request message
            using (var bWriter = new System.IO.BinaryWriter(stream, utf8, true))
            {
                var bytes = requestMsg.ToBytes();
                bWriter.Write(bytes.Length);
                bWriter.Write(bytes);
            }
            Console.WriteLine($"Client says: send request {requestMsg}");

            // 02-02 Get response message
            using (var bReader = new System.IO.BinaryReader(stream, utf8))
            {
                var length = bReader.ReadInt32();
                var bytes = bReader.ReadBytes(length);
                var responseMsg = Message.BytesToMessage(bytes);
                Console.WriteLine($"Client says: receive response {responseMsg}");
                return responseMsg;
            }
        }
    }
}
