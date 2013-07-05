using System;

namespace CommonLib
{
    public enum ProtocolChoice
    {
        OneWayDefault,
        OneWayCustom,
        DuplexDefault,
        DuplexCustom,
        DuplexDefaultSynchronized,
        DuplexCustomSynchronized,
    };

    public class Consts
    {
        //Number of messages the client will send to the server
        public const int MessageCount = 100000;

        //the type of protocol desired for communication between client and server
        public readonly static ProtocolChoice ProtocolChoice = ProtocolChoice.OneWayCustom;

        /// <summary>
        /// Simple progress dot on the command line to see that the application is doing anything.
        /// </summary>
        /// <param name="currentMessageCount"></param>
        public static void PrintProgress(int currentMessageCount)
        {
            if (currentMessageCount % 1000 == 0)
            {
                Console.Write(".");
            }
            if (currentMessageCount == MessageCount-1)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints a simple summary of how many messages were sent, how long it took, and what the resulting messages/second count is.
        /// </summary>
        /// <param name="elapsedMilliseconds"></param>
        public static void PrintStats(double elapsedMilliseconds)
        {
            Console.WriteLine("{0} messages is received in {1} ms ({2:0.00} messages/s)",
                MessageCount, elapsedMilliseconds, MessageCount / elapsedMilliseconds);
        }
    }
}
