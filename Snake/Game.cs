﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    //keeps track of the game status and other game related data
    class Game
    {
        const string SnakeWelcome = "Welcome to Ssssnake";
        const string SnakeEnd = "The Snake died. Ssssanks for playing";

        private List<ConsoleColor> WelcomeColors { get; set; }
        private bool GameStatus { get; set; }
        private int GameScore { get; set; }
        private int BiteCount { get; set; }
        private int GameSpeed { get; set; } //in milli seconds
        private int MetaDataScore { get; set; }
        private int MetaDataBodyCount { get; set; }
        private int MetaDataSnakePosition { get; set; }
        private int FieldSize { get; set; }

        public Game(int fieldSize)
        {
            GameStatus = true;
            GameScore = 0;
            BiteCount = 0;
            GameSpeed = 500;
            WelcomeColors = new List<ConsoleColor> { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.DarkGreen };

            FieldSize = fieldSize;
            MetaDataScore = fieldSize;
            MetaDataBodyCount = MetaDataScore + 1;
            MetaDataSnakePosition = MetaDataBodyCount + 1;
        }

        #region Methods
        public bool GetGameStatus()
        {
            return GameStatus;
        }

        public void SetGameStatus(bool gameStatus)
        {
            GameStatus = gameStatus;
        }

        public void DisplaySnakePosition(int xPosition, int yPosition)
        {
            Console.SetCursorPosition(0, MetaDataSnakePosition);
            Console.WriteLine("{0}: X Position: {1}_", MetaDataSnakePosition, xPosition);
            Console.Write("{0}: Y Position: {1}_", MetaDataSnakePosition + 1, yPosition);
        }

        public void DisplayGameScore()
        {
            Console.SetCursorPosition(0, MetaDataScore);
            Console.WriteLine("{0}: Game Score: {1}", MetaDataScore, GameScore);
        }

        public void DisplaySnakeBodyCount(int snakeBody)
        {
            Console.SetCursorPosition(0, MetaDataBodyCount);
            Console.WriteLine("{0}: Snake Body: {1}", MetaDataBodyCount, snakeBody);
        }

        public int GetGameScore()
        {
            return GameScore;
        }

        public void SetGameScore(int points)
        {
            GameScore = GetGameScore() + points;
        }

        public void UpdateBiteCount()
        {
            BiteCount++;
        }

        public int GetBiteCount()
        {
            return BiteCount;
        }

        public void UpdateGameSpeed(bool update)
        {
            if (update == true && GameSpeed > 100)
            {
                GameSpeed -= 100;
            }
        }

        public int GetGameSpeed()
        {
            return GameSpeed;
        }

        public void DisplayWelcome()
        {
            int consoleColorIndex = 0;
            for(int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(i * 5, i);
                Console.ForegroundColor = WelcomeColors[consoleColorIndex];
                Console.Write(SnakeWelcome);
                Thread.Sleep(1000);

                if (i == WelcomeColors.Count - 1)
                {
                    consoleColorIndex = 0;
                }
                else
                {
                    consoleColorIndex++;
                }
                Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
        }

        public void DisplayEnd()
        {
            int consoleColorIndex = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i > 0)
                {
                    Console.SetCursorPosition(FieldSize, i -1);
                    Console.Write(new string(' ', SnakeEnd.Count() + (i * 10)));
                }

                Console.SetCursorPosition(FieldSize + (i * 5), i);
                Console.ForegroundColor = WelcomeColors[consoleColorIndex];
                Console.Write(SnakeEnd);
                Thread.Sleep(1000);

                if (i == WelcomeColors.Count - 1)
                {
                    consoleColorIndex = 0;
                }
                else
                {
                    consoleColorIndex++;
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        #endregion
    }
}
