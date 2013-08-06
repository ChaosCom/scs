using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hik.Communication.ScsServices.Service;

namespace Test02CommonLib
{
    /// <summary>
    /// Client -> Server method call interface. This represents the service calls the server offers to its clients.
    /// </summary>
    [ScsService(Version="1.0.0.0")]
    public interface IChatService
    {
        void Login(UserInfo info);
        void Logout();

        void SendMessageToRoom(ChatMessage message);
        void SendPrivateMessage(string sendToNick, ChatMessage message);
        void ChangeStatus(UserStatus status);

    }
}
