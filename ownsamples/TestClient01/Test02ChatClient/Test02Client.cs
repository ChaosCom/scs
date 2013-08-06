using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hik.Communication.ScsServices.Client;
using Test02CommonLib;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using System.Threading;

namespace Test02ChatClient
{
    class Test02Client : IChatClient
    {
        static IScsServiceClient<IChatService> scs;

        static void Main(string[] args)
        {
        	//cannot use 'this' as 2nd parameter
            //scs = ScsServiceClientBuilder.CreateClient<IChatService>(
            //    new ScsTcpEndPoint("127.0.0.1", 10048), new Test02Client());


            //scs.Connected +=new EventHandler(_scsClient_Connected);
            //scs.Disconnected +=new EventHandler(_scsClient_Disconnected);

            //scs.Connect();

            //Thread.Sleep(5000);
            //scs.ServiceProxy.SendMessageToRoom(new ChatMessage("Hi Everyone"));
            //Console.ReadKey();
    		
    		var client = new ChatClient();
    		client.ConnectionTest();
        }

        static void _scsClient_Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Client disconnected");
        }

        static void _scsClient_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("Client connected");
            Random r  = new Random();
            scs.ServiceProxy.Login(new UserInfo { Nick=r.Next().ToString() });

        }

        public void OnUserLogin(UserInfo info)
        {
            Console.WriteLine("OnUserLogin: {0}", info.Nick);
        }

        public void OnUserLogout()
        {
            Console.WriteLine("OnUserLogout");
        }

        public void OnMessageToRoom(string nicknameFrom, ChatMessage message)
        {
            Console.WriteLine("OnMessageToRoom: {0}: {1}", nicknameFrom, message.MessageText);
        }

        public void OnPrivateMessage(string nicknameFrom, ChatMessage message)
        {
            Console.WriteLine("OnPrivateMessage: {0}: {1}", nicknameFrom, message.MessageText);
        }

        public void OnUserStatusChange()
        {
            throw new NotImplementedException();
        }

        public void GetUserList(UserInfo[] users)
        {

        }
    }
}
