using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //Have different types of fruit - make use of the template pattern
    public abstract class Fruit
    {
        #region Properties
        static readonly Random random = new Random();

        private ConsoleColor FruitColor { get; set; }
        private string FruitIcon { get; set; }
        private int FruitPoints { get; set; }
        private bool IsEaten { get; set; }
        private bool UpdateSpeed { get; set; }
        private bool FruitPositionIsGood { get; set; }
        private bool IsBerry { get; set; }
        private int BerryMoves { get; set; }

        private int XPosition { get; set; }
        private int YPosition { get; set; }
        private int FieldSize { get; set; }
        #endregion

        public Fruit(int fieldSize)
        {
            FieldSize = fieldSize;
            IsEaten = false;
            UpdateSpeed = false;
            FruitPositionIsGood = false;
        }

        #region Methods Implemented in Concrete Fruit classes
        public abstract string SetFruitIcon();
        public abstract int SetFruitPoints();
        public abstract ConsoleColor SetFruitColor();
        #endregion

        #region Methods
        //Template method to create all concrete Fruit instances
        public void CreateFruit(int snakeHeadX, int snakeHeadY, List<int> snakeBodyX, List<int> snakeBodyY)
        {
            FruitPositionIsGood = false;
            FruitIcon = SetFruitIcon();
            FruitPoints = SetFruitPoints();
            FruitColor = SetFruitColor();
            while (!FruitPositionIsGood)
            {
                SetXPosition();
                SetYPosition();
                CheckFruitPosition(snakeHeadX, snakeHeadY, snakeBodyX, snakeBodyY);
            }
            DrawFruit();
        }

        public void DrawFruit()
        {
            Console.ForegroundColor = GetFruitColor();
            Console.SetCursorPosition(GetYPosition(), GetXPosition());
            Console.Write(GetFruitIcon());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public int GetFruitPoints()
        {
            return FruitPoints;
        }

        public string GetFruitIcon()
        {
            return FruitIcon;
        }

        public ConsoleColor GetFruitColor()
        {
            return FruitColor;
        }

        public int CreateRandomInt()
        {
            return random.Next(1, FieldSize - 1);
        }

        public void SetXPosition()
        {
            XPosition = CreateRandomInt();
        }

        public int GetXPosition()
        {
            return XPosition;
        }

        public void SetYPosition()
        {
            YPosition = CreateRandomInt();
        }

        public int GetYPosition()
        {
            return YPosition;
        }

        public void CheckFruitPosition(int snakeHeadX, int snakeHeadY, List<int> snakeBodyX, List<int> snakeBodyY)
        {
            bool noPositionClash = true;
            if (XPosition == snakeHeadX && YPosition == snakeHeadY)
            {
                noPositionClash = false;
            }

            if (noPositionClash == true)
            {
                for (int i = 0; i < snakeBodyX.Count(); i++)
                {
                    if (XPosition == snakeBodyX[i] && YPosition == snakeBodyY[i])
                    {
                        noPositionClash = false;
                    }
                }
            }

            if (noPositionClash == true)
            {
                FruitPositionIsGood = true;
            }
        }

        public void SetIsEaten(bool isEaten)
        {
            IsEaten = isEaten;
        }

        public bool GetIsEaten()
        {
            return IsEaten;
        }

        public void DetectFruitCollision(int snakeX, int snakeY)
        {
            if (snakeX == XPosition && snakeY == YPosition)
            {
                SetIsEaten(true);
            }
        }

        public void SetUpdateSpeed(bool update)
        {
            UpdateSpeed = update;
        }

        public bool GetUpdateSpeed()
        {
            return UpdateSpeed;
        }

        public void RemoveOldFruit()
        {
            SetUpdateSpeed(false);

            Console.SetCursorPosition(YPosition, XPosition);
            Console.Write(" ");
        }

        public void SetIsBerry(bool berry)
        {
            IsBerry = berry;
        }

        public bool GetIsBerry()
        {
            return IsBerry;
        }

        public void SetBerryMoves(int moves)
        {
            BerryMoves = moves;
        }

        public void UpdateBerryMoves()
        {
            BerryMoves--;
        }

        public int GetBerryMoves()
        {
            return BerryMoves;
        }
        #endregion

    }

    #region Concrete Fruit Classes
    public class Apple : Fruit
    {

        public Apple(int fieldSize) : base(fieldSize)
        {
            SetIsBerry(false);
        }

        public override ConsoleColor SetFruitColor()
        {
            return ConsoleColor.Green;
        }

        public override string SetFruitIcon()
        {
            return "@";
        }

        public override int SetFruitPoints()
        {
            return 1;
        }
    }

    public class Banana : Fruit
    {
        public Banana(int fieldSize) : base(fieldSize)
        {
            SetIsBerry(false);
        }

        public override ConsoleColor SetFruitColor()
        {
            return ConsoleColor.DarkYellow;
        }

        public override string SetFruitIcon()
        {
            return "(";
        }

        public override int SetFruitPoints()
        {
            return 3;
        }
    }

    public class Berry : Fruit
    {
        public Berry(int fieldSize) : base(fieldSize)
        {
            SetUpdateSpeed(true);
            SetIsBerry(true);
            SetBerryMoves(30);
        }

        public override ConsoleColor SetFruitColor()
        {
            return ConsoleColor.Red;
        }

        public override string SetFruitIcon()
        {
            return "&";
        }

        public override int SetFruitPoints()
        {
            return 5;
        }
    }
    #endregion
}
