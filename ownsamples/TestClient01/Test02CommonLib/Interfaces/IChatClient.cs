using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test02CommonLib
{
    /// <summary>
    /// Server -> Client method call interface. This represents the "callbacks" and methods that the server uses on its client proxies 
    /// to communicate certain state back to the clients.
    /// You might notice the logical dependency between IChatService and IChatClient (e.g "Login" method and "OnUserLogin" etc)
    /// </summary>
    public interface IChatClient
    {
        void OnUserLogin(UserInfo info);
        void OnUserLogout();

        void OnMessageToRoom(string nicknameFrom, ChatMessage message);
        void OnPrivateMessage(string nicknameFrom, ChatMessage message);

        void OnUserStatusChange();

        //The only method without a corresponding counterpart in IChatService: Get the list of logged-in users from the server.
        void GetUserList(UserInfo[] users);
    }
}
