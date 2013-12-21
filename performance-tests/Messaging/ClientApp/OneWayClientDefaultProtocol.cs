using System;
using CommonLib;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;

namespace ClientApp
{
    class OneWayClientDefaultProtocol
    {
        public static void Run()
        {
            Console.WriteLine("Press enter to connect to server and send " + Consts.MessageCount + " messages.");
            Console.ReadLine();
            Console.WriteLine("Sending");
            using (var client = ScsClientFactory.CreateClient(Consts.ServerEndpoint))
            {
                client.Connect();

                for (var i = 0; i < Consts.MessageCount; i++)
                {
                    Consts.PrintProgress(i);
                    client.SendMessage(new ScsTextMessage("Hello from client!"));
                }

                Console.WriteLine("Press enter to disconnect from server");
                Console.ReadLine();
            }
        }
    }
}
