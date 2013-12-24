using System;
using System.Diagnostics;
using CommonLib;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;

namespace ClientApp
{
    class DuplexClientDefaultProtocol
    {
        private static int _messageCount;
        private static Stopwatch _stopwatch;

        public static void Run()
        {
            //Console.WriteLine("Press enter to connect to server and send " + Consts.MessageCount + " messages.");
            //Console.ReadLine();
            using (var client = ScsClientFactory.CreateClient(Consts.ServerEndpoint))
            {
                client.MessageReceived += client_MessageReceived;

                try
                {
                    Console.WriteLine("Connecting to " + Consts.ServerEndpoint);
                    client.Connect();

                    Console.WriteLine("Sending " + Consts.MessageCount + " messages.");
                    for (var i = 0; i < Consts.MessageCount; i++)
                    {
                        Consts.PrintProgress(i);
                        client.SendMessage(new ScsTextMessage("Hello from client!"));
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                Console.WriteLine("Press enter to close application.");
                Console.ReadLine();
            }
        }

        static void client_MessageReceived(object sender, MessageEventArgs e)
        {
            ++_messageCount;

            if (_messageCount % Consts.MessageCount == 1)
            {
                _stopwatch = Stopwatch.StartNew();
            }
            else if (_messageCount % Consts.MessageCount == 0)
            {
                _stopwatch.Stop();
                Consts.PrintStats(_stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
