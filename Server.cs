using System;

namespace tcpip_ex01_dotnetcore
{
    class Server
    {
        private bool isStart = false;
        private System.Net.IPEndPoint endpoint;
        private System.Net.Sockets.TcpListener listener;

        public Server(System.Net.IPEndPoint endpoint)
        {
            this.isStart = false;
            this.endpoint = endpoint;
            // Auto start the server.
            this.Start();
        }

        public void Start()
        {
            // 01 Listen to the endpoint
            this.listener = new System.Net.Sockets.TcpListener(this.endpoint);
            try
            {
                this.listener.Stop();
                this.listener.Start();
            } catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine($"Server says: Can not listen local:'{this.endpoint.Port}'. Quit.");
                var errorMessage = new Message(99, $"Server can not listen local:'{this.endpoint.Port}'.");
                return;
            }
            this.isStart = true;
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Server started at '{System.DateTime.Now}'");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            // 02 (Loop)Receive the request and send response.
            while(true)
            {
                Message requestMsg = null;
                Message responseMsg = null;
                var utf8 = System.Text.Encoding.UTF8;

                // 02-01 Receive the request
                using (var stream = this.listener.AcceptTcpClient().GetStream())
                using (var bReader = new System.IO.BinaryReader(stream, utf8))
                {
                    requestMsg = this.Read(bReader);
                    Console.WriteLine($"Server says: received {requestMsg}");
                }
                
                // 02-02 Process the request message
                responseMsg = this.ProcessMsg(requestMsg);

                // 02-03 Send the response message
                using (var stream = this.listener.AcceptTcpClient().GetStream())
                using (var bWriter = new System.IO.BinaryWriter(stream, utf8))
                {
                    this.Write(bWriter, responseMsg);
                    Console.WriteLine($"Server says: sent '{responseMsg}'");
                }
            }

        }

        private Message Read(System.IO.BinaryReader bReader)
        {
            var length = bReader.ReadInt32();
            var bytes = bReader.ReadBytes(length);
            return this.BytesToMessage(bytes);
        }

        private Message ProcessMsg(Message requestMsg)
        {
            var responseMsg = new Message
            {
                id = requestMsg.id + 1,
                content = $"'{requestMsg.content}' processed"
            };
            return responseMsg;
        }

        private void Write(System.IO.BinaryWriter bWriter, Message responseMsg)
        {
            var bytes = this.MessageToBytes(responseMsg);
            bWriter.Write(bytes.Length);
            bWriter.Write(bytes);
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

        public bool IsStart()
        {
            return this.isStart;
        }

        public void Stop()
        {
            this.listener.Stop();
            this.isStart = false;
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Server stopped at '{System.DateTime.Now}'");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
        }
    }
}
