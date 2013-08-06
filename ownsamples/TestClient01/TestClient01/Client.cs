using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.ScsServices.Client;
using CommonLib;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;

namespace TestClient01
{
    class Client
    {
        static void Main(string[] args)
        {
            var client = ScsServiceClientBuilder.CreateClient<ICommonMutualService>(
                new ScsTcpEndPoint("127.0.0.1", Consts.DefaultPort));


            client.Connect();

            //print out registered sessions
            PrintSessions(client.ServiceProxy.DiscoverHostedSessions());

            var serviceToken = HelperFunctions.GenerateToken();
            Console.WriteLine("Announcing {0}", serviceToken);
            var token = client.ServiceProxy.AnnounceService(serviceToken);

            //print out registered sessions
            PrintSessions(client.ServiceProxy.DiscoverHostedSessions());

            //try to deannounce a service with a wrong token
            client.ServiceProxy.DeannounceService("ASDF");
            PrintSessions(client.ServiceProxy.DiscoverHostedSessions());

            //now correctly deannounce the service
            //client.ServiceProxy.DeannounceService(token);

            //print out registered sessions
            PrintSessions(client.ServiceProxy.DiscoverHostedSessions());

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        private static void PrintSessions(IEnumerable<HostedSession> sessions)
        {
            Console.WriteLine("Found {0} sessions:", sessions.Count());
            foreach (var session in sessions)
            {
                Console.WriteLine(session.Name);
            }
        }
    }
}
