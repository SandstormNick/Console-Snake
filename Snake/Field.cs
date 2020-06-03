using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //The parameters that the snake can roam in
    class Field
    {
        private const int InnerWall = 0;

        private int FieldSize { get; set; }
        private string TopBottomWallSymbol { get; set; }
        private string SideWallSymbol { get; set; }

        public Field()
        {
            FieldSize = 30;
            TopBottomWallSymbol = "*";
            SideWallSymbol = "|";
        }

        #region Methods
        public void SetField()
        {
            for (int i = 0; i < FieldSize; i++)
            {
                for (int x = 0; x < FieldSize; x++)
                {
                    if (i == 0 || i == (FieldSize - 1))
                    {
                        Console.Write(TopBottomWallSymbol);
                    }
                    if ((i > 0 && i < (FieldSize - 1) && x == 0) || (i > 0 && i < (FieldSize - 1) && x == (FieldSize - 1)))
                    {
                        Console.Write(SideWallSymbol);
                    }
                    if (i > 0 && i < FieldSize - 1 && x > 0 && x < (FieldSize - 1))
                    {
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }
        }

        public int GetFieldSize()
        {
            return FieldSize;
        }

        public bool DetectWallCollisions(int xPosition, int yPosition)
        {
            if (xPosition == InnerWall || yPosition == InnerWall)
            {
                return false;
            }

            if (xPosition == FieldSize - 1 || yPosition == FieldSize - 1)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
