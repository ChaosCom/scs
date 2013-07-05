﻿using System;
using System.Diagnostics;
using CommonLib;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace ServerApp
{
    public class DuplexServerDefaultProtocol
    {
        private static int _messageCount;
        private static Stopwatch _stopwatch;

        public static void Run()
        {
            var server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(10033));
            server.ClientConnected += server_ClientConnected;

            server.Start();

            Console.WriteLine("Press enter to stop server");
            Console.ReadLine();

            server.Stop();
        }

        static void server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            e.Client.MessageReceived += Client_MessageReceived;
        }

        static void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            ++_messageCount;

            var client = (IScsServerClient) sender;
            client.SendMessage(new ScsTextMessage("Hello from server!"));

            if (_messageCount == 1)
            {
                _stopwatch = Stopwatch.StartNew();
            }
            else if (_messageCount == Consts.MessageCount)
            {
                _stopwatch.Stop();
                Consts.PrintStats(_stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
