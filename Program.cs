using System;

namespace tcpip_ex01_dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            var hello = new Message(10, "Hello");
            var world = new Message(20, "World with class Message");

            Console.WriteLine($"{hello.content} {world.content}!");

            Console.Write(hello);
            Console.Write(world);
            Console.WriteLine();
        }
    }
}
