using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorCommonLib
{
    public class Consts
    {
        public const int MethodCallCount = 100000;

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
            if (currentMessageCount == MethodCallCount-1)
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
                MethodCallCount, elapsedMilliseconds, MethodCallCount / elapsedMilliseconds * 1000);
        }

    }
}
