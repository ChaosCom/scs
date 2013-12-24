using System;
using CommonLib;

namespace ServerApp
{
    public class Program
    {
        static void Main()
        {
            //make your "configuration" choice of the protocol type in CommonLib.Consts.ProtocolChoice
            //must be in sync with the server choice (taken care of via switch in server Program.cs as well)
            switch (Consts.ProtocolChoice)
            {
                case ProtocolChoice.OneWayCustomPipe:
                    OneWayServerCustomPipe.Run();
                    break;
                case ProtocolChoice.OneWayDefault:
                    OneWayServerDefaultProtocol.Run();
                    break;
                case ProtocolChoice.OneWayCustom:
                    OneWayServerCustomProtocol.Run();
                    break;
                case ProtocolChoice.DuplexDefault:
                case ProtocolChoice.DuplexDefaultSynchronized:
                    DuplexServerDefaultProtocol.Run();
                    break;
                case ProtocolChoice.DuplexCustom:
                case ProtocolChoice.DuplexCustomSynchronized:
                    DuplexServerCustomProtocol.Run();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
