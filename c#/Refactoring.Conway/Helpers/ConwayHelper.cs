using System;
using System.Threading;

namespace Refactoring.Conway
{
    public static class ConwayHelper
    {
        public static void Tick()
        {
            Thread.Sleep(TimeSpan.FromSeconds(value: 1));
        }

        public static int ReadNumber(string prompt)
        {
            int output = 0;
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
            }
            while (!int.TryParse(input, out output));

            return output;
        }
    }
}
