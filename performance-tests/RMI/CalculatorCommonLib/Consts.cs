using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorCommonLib
{
    public class Consts
    {
        public const int MethodCallCount = 100000;

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

    }
}
