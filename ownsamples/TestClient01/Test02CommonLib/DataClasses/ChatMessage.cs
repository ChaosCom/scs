using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test02CommonLib
{
    [Serializable]
    public class ChatMessage
    {
        public string MessageText { get; set; }

        public ChatMessage()
        {
            MessageText = "";
        }

        public ChatMessage(string message)
        {
            MessageText  = message;
        }
    }
}
