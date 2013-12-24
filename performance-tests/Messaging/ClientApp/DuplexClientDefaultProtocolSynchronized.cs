using System;
using System.Diagnostics;
using CommonLib;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;

namespace ClientApp
{
    class DuplexClientDefaultProtocolSynchronized
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

                using (var synchronizedMessenger = new SynchronizedMessenger<IScsClient>(client))
                {
                    synchronizedMessenger.Start();
                    try
                    {
                        Console.WriteLine("Connecting to " + Consts.ServerEndpoint);
                        client.Connect();

                        Console.WriteLine("Sending " + Consts.MessageCount + " messages.");
                        for (var i = 0; i < Consts.MessageCount; i++)
                        {
                            Consts.PrintProgress(i);
                            synchronizedMessenger.SendMessage(new ScsTextMessage("Hello from client!"));
                            var reply = synchronizedMessenger.ReceiveMessage<ScsTextMessage>();
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }

                Console.WriteLine("Press enter to close application.");
                Console.ReadLine();
            }
        }

        private static void client_MessageReceived(object sender, MessageEventArgs e)
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
