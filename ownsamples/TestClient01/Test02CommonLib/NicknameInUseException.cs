using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Test02CommonLib
{
    /// <summary>
    /// Thrown by the server if a user tries to login with a username that is already being in use.
    /// </summary>
    [Serializable]
    public class NicknameInUseException : ApplicationException
    {
        public NicknameInUseException() { }

        public NicknameInUseException(SerializationInfo s, StreamingContext c) : base(s,c) {}

        public NicknameInUseException(string message) : base(message) { }
    }
}
