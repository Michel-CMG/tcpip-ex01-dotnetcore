using System;

namespace tcpip_ex01_dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            // local
            var ip = System.Net.IPAddress.Loopback;
            var endpoint = new System.Net.IPEndPoint(ip, 53562);

            Console.WriteLine("\n\n\n");
            Console.WriteLine("Select run the server or run the client");
            Console.WriteLine("0: Run the server");
            Console.WriteLine("Any other: Run the client");
            Console.WriteLine();

            string command = Console.ReadLine();
            switch(command)
            {
                case "0":
                    StartServer(endpoint);
                    break;
                default :
                    StartClient(endpoint);
                    break;
            }
        }

        private static void StartServer(System.Net.IPEndPoint endpoint)
        {
            var server = new Server(endpoint);
            server.Stop();
        }

        private static void StartClient(System.Net.IPEndPoint endpoint)
        {
            var client = new Client(endpoint);
            client.Send(new Message(10, "あいうえお"));
            client.Send(new Message(20, "かきくけこ"));
            client.Send(new Message(00, "stop the server"));
        }
    }
}
