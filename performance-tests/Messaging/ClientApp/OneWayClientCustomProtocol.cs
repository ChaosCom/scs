using System;
using CommonLib;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;

namespace ClientApp
{
    class OneWayClientCustomProtocol
    {
        public static void Run()
        {
            //Console.WriteLine("Press enter to connect to server and send " + Consts.MessageCount + " messages.");
            //Console.ReadLine();
            Console.WriteLine("Sending");
            using (var client = ScsClientFactory.CreateClient(Consts.ServerEndpoint))
            {
                client.WireProtocol = new MyWireProtocol();

                client.Connect();

                //create a message containing a specified number of random bytes
                Random r = new Random();
                byte[] randomBytes = new byte[250];
                r.NextBytes(randomBytes);
                var message = randomBytes.ToString();

                //send the message many times to the server
                for (var i = 0; i < Consts.MessageCount; i++)
                {
                    Consts.PrintProgress(i);
                    //client.SendMessage(new ScsTextMessage("Hello from client!"));
                    //client.SendMessage(new ScsTextMessage(message));
                    client.SendMessage(new ScsRawDataMessage(randomBytes));
                }

                Console.WriteLine("Press enter to disconnect from server");
                Console.ReadLine();
            }
        }
    }
}
