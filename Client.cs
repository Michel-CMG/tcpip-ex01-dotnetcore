using System;

namespace tcpip_ex01_dotnetcore
{
    class Client
    {
        public Message Send(Message requestMsg)
        {
            // Do nothing but print the request and response Message
            Console.WriteLine($"Client says: send request '{requestMsg}'");

            var responseMsg = new Message
            {
                id = requestMsg.id,
                content = requestMsg.content
            };

            Console.WriteLine($"Client says: receive response '{responseMsg}'");
            return responseMsg;
        }
    }
}
