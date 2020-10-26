using System;
using System.Security.Cryptography;
using System.Threading;

namespace Refactoring.Conway
{
    static class BoardHelper
    {
        public static Size Size { get; set; }

        public static bool[,] ReInitializeBoard(Size size, bool[,] board)
        {
            bool[,] newBoard = new bool[size.Width, size.Height];
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    int livingNeighbourCount = 0;
                    for (int xScan = x - 1; xScan < x + 2; xScan++)
                    {
                        if (xScan < 0 || xScan >= size.Width)
                        {
                            continue;
                        }
                        for (int yScan = y - 1; yScan < y + 2; yScan++)
                        {
                            if (xScan == x && yScan == y)
                            {
                                continue;
                            }

                            if (yScan >= 0 && yScan < size.Height && board[xScan, yScan])
                            {
                                livingNeighbourCount += 1;
                            }
                        }
                    }
                    newBoard[x, y] = (board[x, y] && livingNeighbourCount == 2) || livingNeighbourCount == 3;
                }
            }

            return newBoard;
        }

        public static bool[,] InitializeBoard(Size size)
        {
            bool[,] board = new bool[size.Width, size.Height];
            int total = (size.Width * size.Height);
            int ratio = (total * 40) / 100;

            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    board[x, y] = RandomNumberGenerator.GetInt32(0, total) < ratio;
                }
            }

            return board;
        }


        public static int GenerationCount(CancellationTokenSource cancellationTokenSource, Size size, int generations, ref bool[,] board)
        {
            int i;
            for (i = 1; i <= generations && !cancellationTokenSource.IsCancellationRequested; i++)
            {
                Console.Clear();
                Console.WriteLine($"Generation: {i}");

                if (IsSocietyAlive(size, board))
                {
                    Console.WriteLine("I guess that's the end of our little society.");
                    break;
                }

                board = ReInitializeBoard(size, board);

                ConwayHelper.Tick();
            }

            return i;
        }

        public static bool IsSocietyAlive(Size size, bool[,] board)
        {
            var isDead = true;
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    var cell = new Cell(board[x, y]);
                    Console.Write(cell.ToString());

                    if (cell.IsAlive)
                    {
                        isDead = false;
                    }

                    if (y == board.GetLength(dimension: 1) - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            return isDead;
        }
    }
}
