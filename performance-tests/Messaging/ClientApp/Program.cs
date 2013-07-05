using System;
using CommonLib;

namespace ClientApp
{
    class Program
    {
        static void Main()
        {
            //make your "configuration" choice of the protocol type in CommonLib.Consts.ProtocolChoice
            //must be in sync with the server choice (taken care of via switch in server Program.cs as well)

            switch (Consts.ProtocolChoice)
            {
                case ProtocolChoice.OneWayDefault:
                    OneWayClientDefaultProtocol.Run();
                    break;
                case ProtocolChoice.OneWayCustom:
                    OneWayClientCustomProtocol.Run();
                    break;
                case ProtocolChoice.DuplexDefault:
                    DuplexClientDefaultProtocol.Run();
                    break;
                case ProtocolChoice.DuplexCustom:
                    DuplexClientCustomProtocol.Run();
                    break;
                case ProtocolChoice.DuplexDefaultSynchronized:
                    DuplexClientDefaultProtocolSynchronized.Run();
                    break;
                case ProtocolChoice.DuplexCustomSynchronized:
                    DuplexClientCustomProtocolSynchronized.Run();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
