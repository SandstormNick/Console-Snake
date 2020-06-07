using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //keeps track of the game status and other game related data
    class Game
    {
        private bool GameStatus { get; set; }
        private int GameScore { get; set; }
        private int BiteCount { get; set; }

        public Game()
        {
            GameStatus = true;
            GameScore = 0;
            BiteCount = 0;
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
            Console.SetCursorPosition(0, 35);
            Console.WriteLine("X Position: {0}_", xPosition);
            Console.Write("Y Position: {0}_", yPosition);
        }

        public void DisplayGameScore()
        {
            Console.SetCursorPosition(0, 33);
            Console.Write("Game Score: {0}", GameScore);
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
        #endregion
    }
}
