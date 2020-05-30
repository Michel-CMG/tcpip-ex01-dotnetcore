using System;

namespace tcpip_ex01_dotnetcore
{
    class Server
    {
        private bool isStart = false;
        private System.Net.IPEndPoint endpoint;

        public Server(System.Net.IPEndPoint endpoint)
        {
            this.isStart = false;
            this.endpoint = endpoint;
            // Auto start the server.
            this.Start();
        }

        public void Start()
        {
            this.isStart = true;
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Server started at '{System.DateTime.Now}'");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
        }

        public bool IsStart()
        {
            return this.isStart;
        }

        public void Stop()
        {
            this.isStart = false;
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Server stopped at '{System.DateTime.Now}'");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
        }
    }
}
