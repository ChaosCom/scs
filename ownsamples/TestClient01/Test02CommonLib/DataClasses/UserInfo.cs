using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test02CommonLib
{
    [Serializable]
    public class UserInfo
    {
        public string Nick { get; set; }
        public UserStatus Status { get; set; }
    }

    [Serializable]
    public enum UserStatus
    {
        Available,
        Busy,
        Out,
    }
}
