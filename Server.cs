using System;
using System.Net.Sockets;

namespace tcpip_ex01_dotnetcore
{
    class Server
    {
        private bool isRunning = false;
        private System.Net.IPEndPoint endpoint;
        private System.Net.Sockets.TcpListener listener;

        public Server(System.Net.IPEndPoint endpoint)
        {
            this.isRunning = false;
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
                this.isRunning = true;
            } catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine($"Server says: Can not listen local:'{this.endpoint.Port}'. Quit.");
                var errorMessage = new Message(99, $"Server can not listen local:'{this.endpoint.Port}'.");
                return;
            }
            
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Server started at '{System.DateTime.Now}'");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            // 02 (Loop)Receive the request and send response.
            while(this.IsRunning())
            {
                using (var stream = this.listener.AcceptTcpClient().GetStream())
                {
                    this.ReadProcessWrite(stream);
                }
            }
        }

        private void ReadProcessWrite(NetworkStream stream)
        {
            Message requestMsg = null;
            Message responseMsg = null;
            var utf8 = System.Text.Encoding.UTF8;

            // 02-01 Receive the request
            using (var bReader = new System.IO.BinaryReader(stream, utf8, true))
            {
                var length = bReader.ReadInt32();
                var bytes = bReader.ReadBytes(length);
                requestMsg = Message.BytesToMessage(bytes);
                Console.WriteLine($"Server says: receive {requestMsg}");
            }
            
            // 02-02 Process the request message
            responseMsg = this.ProcessMsg(requestMsg);

            // 02-03 Send the response message
            using (var bWriter = new System.IO.BinaryWriter(stream, utf8))
            {
                var bytes = responseMsg.ToBytes();
                bWriter.Write(bytes.Length);
                bWriter.Write(bytes);
                Console.WriteLine($"Server says: send {responseMsg}");
            }

            if (requestMsg.id == 00 && responseMsg.id == 00)
            {
                this.Stop();
                return;
            }
        }

        private Message ProcessMsg(Message requestMsg)
        {
            Message responseMsg = null;
            if (requestMsg.id == 00)
            {
                // The request is to stop the server
                responseMsg = new Message
                {
                    id = 0,
                    content = "server starts to stop"
                };
            } else
            {
                responseMsg = new Message
                {
                    id = requestMsg.id + 1,
                    content = $"'{requestMsg.content}' processed"
                };
            }

            return responseMsg;
        }

        public bool IsRunning()
        {
            return this.isRunning;
        }

        public void Stop()
        {
            // Stop only when server is running.
            if (this.IsRunning())
            {
                this.listener.Stop();
                this.isRunning = false;
                Console.WriteLine();
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"Server stopped at '{System.DateTime.Now}'");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}
