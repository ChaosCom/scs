using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test02CommonLib;
using Hik.Communication.ScsServices.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using System.Threading;

namespace Test02ChatClient
{
    public class ChatClient : IChatClient
    {
        //private ChatClient _chatClient;
        private IScsServiceClient<IChatService> _scsClient;

        public void ConnectionTest()
        {
            //Disconnect if currently connected
            

            //Create a ChatClient to handle remote method invocations by server
            //_chatClient = this;

            //Create a SCS client to connect to SCS server
            //The 2nd parameter is EXTREMELY important to note and must be non-null if you want to have Server -> Client callbacks.
            _scsClient = ScsServiceClientBuilder.CreateClient<IChatService>(
                new ScsTcpEndPoint("127.0.0.1", 10048), this );

            //Register events of SCS client
            _scsClient.Connected += ScsClient_Connected;
            _scsClient.Disconnected += ScsClient_Disconnected;

            //Connect to the server
            _scsClient.Connect();

            Thread.Sleep(5000);

            _scsClient.ServiceProxy.SendMessageToRoom(new ChatMessage("Hi Everyone"));

            Console.ReadKey();

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


        void ScsClient_Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Client was disconnected.. Is the server still running?");
        }

        void ScsClient_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("Client connected");
            Random r  = new Random();
            _scsClient.ServiceProxy.Login(new UserInfo { Nick=r.Next().ToString() });

        }

        //public override string ToString()
        //{
        //    return base.ToString();
        //}

    }
}
