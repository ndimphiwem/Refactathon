﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public class GameOfLife
    {
        private int _width;
        private int _height;
        private int _generations;
        private bool[,] _board;
        private readonly int _percentageBoardToStartAlive;

        public GameOfLife(int width, int height, int generations, int percentageToStartAlive = 40)
        {
            _width = width;
            _height = height;
            _generations = generations;
            _percentageBoardToStartAlive = percentageToStartAlive;
            GenerateBoard();
        }

        private void GenerateBoard()
        {
            _board = new bool[_width, _height];
            var total = _width * _height;
            var ratio = total * _percentageBoardToStartAlive / 100;

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    _board[x, y] = RandomNumberGenerator.GetInt32(0, total) < ratio;
                }
            }
        }

        public IEnumerable<string> Run(CancellationToken cancellationToken)
        {
            for (var i = 0; i <= _generations && !cancellationToken.IsCancellationRequested; i++)
            {
                var sb = new StringBuilder();
                var societyDied = true;

                sb.AppendLine($"Generation: {i}");
                for (var x = 0; x < _width; x++)
                {
                    for (var y = 0; y < _height; y++)
                    {


                        if (_board[x, y])
                        {
                            sb.Append("0");
                            societyDied = false;
                        }
                        else
                        {
                            sb.Append(".");
                        }
                        if (y == _board.GetLength(dimension: 1) - 1)
                        {
                            sb.AppendLine();
                        }
                    }
                }

                if (societyDied)
                {
                    sb.AppendLine("I guess that's the end of our little society.");
                    break;
                }

                var newBoard = new bool[_width, _height];
                for (var x = 0; x < _width; x++)
                {
                    for (var y = 0; y < _height; y++)
                    {
                        var livingNeighbourCount = 0;
                        for (var xScan = x - 1; xScan < x + 2; xScan++)
                        {
                            if (xScan < 0 || xScan >= _width)
                            {
                                continue;
                            }
                            for (var yScan = y - 1; yScan < y + 2; yScan++)
                            {
                                if (xScan == x && yScan == y)
                                {
                                    continue;
                                }

                                if (yScan >= 0 && yScan < _width && _board[xScan, yScan])
                                {
                                    livingNeighbourCount += 1;
                                }
                            }
                        }
                        newBoard[x, y] = _board[x, y] && livingNeighbourCount == 2 || livingNeighbourCount == 3;
                    }
                }

                _board = newBoard;
                Thread.Sleep(TimeSpan.FromSeconds(value: 1));
                yield return sb.ToString();
            }
        }
    }
}