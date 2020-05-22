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
            for (int i = 0; i < 40; i++)
            {
                for (int x = 0; x < 40; x++)
                {
                    if (i == 0 || i == 39)
                    {
                        Console.Write(TopBottomWallSymbol);
                    }
                    if ((i > 0 && i < 39 && x == 0) || (i > 0 && i < 39 && x == 39))
                    {
                        Console.Write(SideWallSymbol);
                    }
                    if (i > 0 && i < 39 && x > 0 && x < 39)
                    {
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
