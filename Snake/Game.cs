using System;
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
        #region Properties
        const string SNAKE_WELCOME = "Welcome to Ssssnake";
        const string REPLAY_MESSAGE = "The Snake died. Press Y to play again.";
        const string SNAKE_END = "Ssssanks for playing";

        private List<ConsoleColor> WelcomeColors { get; set; }
        private bool GameStatus { get; set; }
        private int GameScore { get; set; }
        private int BiteCount { get; set; }
        private int GameSpeed { get; set; } //in milli seconds
        private int MetaDataScore { get; set; }
        private int MetaDataBodyCount { get; set; }
        private int MetaDataSnakePosition { get; set; }
        private int FieldSize { get; set; }
        private bool Reset { get; set; }
        #endregion

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
        public void ResetGame()
        {
            Reset = true;
            GameStatus = true;
            GameScore = 0;
            BiteCount = 0;
            GameSpeed = 500;
        }

        public bool GetReset()
        {
            return Reset;
        }

        public bool GetGameStatus()
        {
            return GameStatus;
        }

        public void SetGameStatus(bool wallCollision, bool snakeCollision)
        {
            if (!wallCollision || !snakeCollision)
            {
                GameStatus = false;
            }
            else
            {
                GameStatus = true;
            }
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

        #region Meta Data during game play
        public void DisplaySnakePosition(int xPosition, int yPosition)
        {
            Console.SetCursorPosition(0, MetaDataSnakePosition);
            Console.WriteLine("X Position: {0}   ", xPosition);
            Console.Write("Y Position: {0}   ", yPosition);
        }

        public void DisplayGameScore()
        {
            Console.SetCursorPosition(0, MetaDataScore);
            Console.WriteLine("Game Score: {0}   ", GameScore);
        }

        public void DisplaySnakeBodyCount(int snakeBody)
        {
            Console.SetCursorPosition(0, MetaDataBodyCount);
            Console.WriteLine("Snake Body: {0}   ", snakeBody);
        }
        #endregion

        public void DisplayWelcome()
        {
            int consoleColorIndex = 0;
            for(int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(i * 5, i);
                Console.ForegroundColor = WelcomeColors[consoleColorIndex];
                Console.Write(SNAKE_WELCOME);
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

        public void DisplayReplayMessage()
        {
            Console.SetCursorPosition(FieldSize, 0);
            Console.Write(REPLAY_MESSAGE);
        }

        public void GetReplayResponse()
        {
            string userResponse = Console.ReadLine().ToUpper();
            if (userResponse == "Y")
            {
                ResetGame();
            }

            Console.SetCursorPosition(FieldSize, 0);
            Console.Write(new string(' ', REPLAY_MESSAGE.Count() + 1));
        }

        public void DisplayEnd()
        {
            int consoleColorIndex = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i > 0)
                {
                    Console.SetCursorPosition(FieldSize, i -1);
                    Console.Write(new string(' ', SNAKE_END.Count() + (i * 10)));
                }

                Console.SetCursorPosition(FieldSize + (i * 5), i);
                Console.ForegroundColor = WelcomeColors[consoleColorIndex];
                Console.Write(SNAKE_END);
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
