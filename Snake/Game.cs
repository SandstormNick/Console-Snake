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

        public Game()
        {
            GameStatus = true;
        }

        #region Methods
        public bool GetGameStatus()
        {
            return GameStatus;
        }
        #endregion
    }
}
