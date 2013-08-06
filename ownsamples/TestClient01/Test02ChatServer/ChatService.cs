using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Test02CommonLib;
using Hik.Communication.ScsServices.Service;
using Hik.Collections;
using System.Threading.Tasks;

namespace Test02ChatServer
{
    /// <summary>
    /// Data Holder Class
    /// </summary>
    public class ChatClient
    {
        public IScsServiceClient Client { get; private set; }
        public IChatClient ClientProxy { get; private set; }
        public UserInfo User { get; private set; }

        public ChatClient(IScsServiceClient client, IChatClient clientProxy, UserInfo userInfo)
        {
            Client = client;
            ClientProxy = clientProxy;
            User = userInfo;
        }
    }

    public class ChatService : ScsService, IChatService
    {
        public event EventHandler UserListChanged;
        private readonly ThreadSafeSortedList<long, ChatClient> _clients;

        public List<UserInfo> UserList
        {
            get
            {
                return (from client in _clients.GetAllItems()
                        select client.User
                       ).ToList();
            }
        }

        public ChatService()
        {
            _clients = new ThreadSafeSortedList<long, ChatClient>();
        }



        #region Private Helpers

        private ChatClient FindClientByNick(string nick)
        {
            return (from client in _clients.GetAllItems()
                    where client.User.Nick == nick
                    select client
                   ).FirstOrDefault();
        }

        private void OnUserListChanged()
        {
            var handler = UserListChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        private void SendUserListToClient(ChatClient client)
        {
            //send the list of Users to a newly joined client, minus its own nick
            var userArray = UserList.Where((user) => user.Nick != client.User.Nick).ToArray();
            if (userArray.Length <= 0) return;
            client.ClientProxy.GetUserList(userArray);
        }


        private void SendNewUserInfoToAllClients(UserInfo info)
        {
            var clients = _clients.GetAllItems();

            foreach (var client in clients)
            {
                if (client.User.Nick == info.Nick) continue;
                try
                {
                    client.ClientProxy.OnUserLogin(info);
                } catch (Exception)
                {

                }
            }
        }

        private void ClientLogout(long clientId)
        {
            var client = _clients[clientId];
            if (client == null) return;

            _clients.Remove(clientId);
            client.Client.Disconnected -= client_Disconnected;

            Task.Factory.StartNew(
                () => 
                {
                    OnUserListChanged();

                    try 
	                {	        
		                string userNick = client.User.Nick;
                    
                        //send user logout info to all remaining clients
                        var chatClients = _clients.GetAllItems();
                        foreach (var c in chatClients)
                        {
                            c.ClientProxy.OnUserLogout();
                        }
	                }
	                catch (Exception)
	                {
	                }
                    
                }
                );

        }

        #endregion



        public void Login(UserInfo info)
        {
            if (FindClientByNick(info.Nick) != null)
            {
                throw new NicknameInUseException(String.Format("Nickname {0} already in use."));
            }
            var client = CurrentClient;
            client.Disconnected += client_Disconnected;

            var proxy =  client.GetClientProxy<IChatClient>();
            var chatClient = new ChatClient(client, proxy, info);

            _clients[client.ClientId] = chatClient;

            Task.Factory.StartNew(
                () => {
                    OnUserListChanged();
                    SendUserListToClient(chatClient);
                    SendNewUserInfoToAllClients(info);
                }
            );
        }

        void client_Disconnected(object sender, EventArgs e)
        {
            var client = (IScsServiceClient) sender;
            ClientLogout(client.ClientId);
        }

        public void Logout()
        {
            ClientLogout(CurrentClient.ClientId);
        }

        public void SendMessageToRoom(ChatMessage message)
        {
            var sender = _clients[CurrentClient.ClientId];
            if (sender == null) return;

            Task.Factory.StartNew(
                () => 
                {
                    try
                    {
                        foreach (var client in _clients.GetAllItems())
                        {
                            client.ClientProxy.OnMessageToRoom(sender.User.Nick, message);
                        }
                    } catch (Exception) { }
                }
            );
        }

        public void SendPrivateMessage(string sendToNick, ChatMessage message)
        {
            var sender = _clients[CurrentClient.ClientId];
            var receiver = FindClientByNick(sendToNick);

            if (sender == null || receiver == null) return;

            receiver.ClientProxy.OnPrivateMessage(sender.User.Nick, message);
        }

        public void ChangeStatus(UserStatus status)
        {
            throw new NotImplementedException();
        }

    }
}
