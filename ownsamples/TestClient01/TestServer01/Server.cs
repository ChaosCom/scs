using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.ScsServices.Service;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using CommonLib;

namespace TestServer01
{
    class Server
    {
        static void Main(string[] args)
        {
            var server = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(Consts.DefaultPort));
            MutualService ms = new MutualService();
            server.AddService<ICommonMutualService, MutualService>(ms);

            server.Start();

            Console.WriteLine("Mutual Service Server started successfully. Press d to dump, q to quit");

            bool quit = false;

            while (!quit)
            {
                var key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case 'd':
                        PrintAnnouncedServices(ms.DiscoverHostedSessions());
                        break;
                    case 'q':
                        quit = true;
                        break;
                    default:
                        break;
                }
            } 

            server.Stop();


        }

        private static void PrintAnnouncedServices(IEnumerable<HostedSession> announcedServices)
        {
            Console.WriteLine("Number of announced services: {0}", announcedServices.Count());
            foreach (var service in announcedServices)
            {
                Console.WriteLine(service.Name);
            }
        }
    }
}
