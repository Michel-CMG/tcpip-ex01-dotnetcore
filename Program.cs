using System;

namespace tcpip_ex01_dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHello();

            Console.WriteLine("\n\n\n");
            Console.WriteLine("Select run the server or run the client");
            Console.WriteLine("0: Run the server");
            Console.WriteLine("Any other: Run the client");
            Console.WriteLine();

            string command = Console.ReadLine();
            switch(command)
            {
                case "0":
                    StartServer();
                    break;
                default :
                    StartClient();
                    break;
            }
        }

        private static void PrintHello()
        {
            var hello = new Message(10, "Hello");
            var world = new Message(20, "World with class Message");

            Console.WriteLine($"{hello.content} {world.content}!");
            Console.Write(hello);
            Console.Write(world);
            Console.WriteLine();
        }

        private static void StartServer()
        {
            var server = new Server();
            Console.WriteLine($"The server running status: {server.IsStart()}");
            // Wait 2 seconds because server is do nothing now
            System.Threading.Thread.Sleep(2000);
            server.Stop();
        }

        private static void StartClient()
        {
            var client = new Client();
            client.Send(new Message(10, "あいうえお"));
            client.Send(new Message(20, "かきくけこ"));
            client.Send(new Message(00, "stop the server"));
        }
    }
}
