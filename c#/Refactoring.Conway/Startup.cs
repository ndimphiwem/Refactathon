using System;
using System.Threading;

namespace Refactoring.Conway
{
    static class Startup
    {
        public static void Execute(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                Size size = new Size();

                size.Width = ConwayHelper.ReadNumber("What is the width of the board?");

                size.Height = ConwayHelper.ReadNumber("What is the height of the board?");

                int generations = ConwayHelper.ReadNumber("How many generations does the board run for");

                bool[,] board = BoardHelper.InitializeBoard(size);

                int generationCount = BoardHelper.GenerationCount(cancellationTokenSource, size, generations, ref board);

                Console.WriteLine($"Generations ({generationCount - 1}) - Output Completed! Press any key to exit.");
                Console.ReadKey();
            }
            catch (OperationCanceledException)
            {
                //DO NOTHING
            }
        }
    }
}
