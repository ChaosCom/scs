using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.ScsServices.Service;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Test02CommonLib;

namespace Test02ChatServer
{
    class Test02Server
    {
        static IScsServiceApplication _app;
        static ChatService _service;

        static void Main(string[] args)
        {


            try
            {
                _app = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(10048));
                _app.ClientConnected += new EventHandler<ServiceClientEventArgs>(_app_ClientConnected);
                _app.ClientDisconnected +=new EventHandler<ServiceClientEventArgs>(_app_ClientDisconnected);


                _service = new ChatService();
                _service.UserListChanged += new EventHandler(_service_UserListChanged);

                _app.AddService<IChatService, ChatService>(_service);
                _app.Start();


                Console.WriteLine("Server Started");
                Console.ReadKey();
            } catch (Exception)
            {
                
            }


        }

        static void _app_ClientDisconnected(object sender, ServiceClientEventArgs e)
        {
            Console.WriteLine("_app_ClientDisconnected: {0}-{1}-{2}", e.Client.ClientId, e.Client.CommunicationState, e.Client.RemoteEndPoint);
            Console.WriteLine("Client object: {0}", e.Client.ToString());
        }

        static void _app_ClientConnected(object sender, ServiceClientEventArgs e)
        {
            Console.WriteLine("_app_ClientConnected: {0}-{1}-{2}", e.Client.ClientId, e.Client.CommunicationState, e.Client.RemoteEndPoint);
            Console.WriteLine("Client object: {0}", e.Client.ToString());
        }

        static void _service_UserListChanged(object sender, EventArgs e)
        {
            Console.WriteLine("UserListChanged, Count: {0}", _service.UserList.Count);
            
        }
    }
}
