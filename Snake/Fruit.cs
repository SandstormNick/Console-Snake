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
        static readonly Random random = new Random();
        private string FruitIcon { get; set; }
        private int FruitPoints { get; set; }
        private ConsoleColor FruitColor { get; set; }
        private bool IsEaten { get; set; }
        private bool UpdateSpeed { get; set; }

        private int XPosition { get; set; }
        private int YPosition { get; set; }
        private int FieldSize { get; set; }

        public Fruit(int fieldSize)
        {
            FieldSize = fieldSize;
            IsEaten = false;
            UpdateSpeed = false;
        }

        public abstract string SetFruitIcon();
        public abstract int SetFruitPoints();
        public abstract ConsoleColor SetFruitColor();

        public void CreateFruit()
        {
            FruitIcon = SetFruitIcon();
            FruitPoints = SetFruitPoints();
            FruitColor = SetFruitColor();
            SetXPosition();
            SetYPosition();
            DrawFruit();
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

        public void DrawFruit()
        {
            Console.ForegroundColor = GetFruitColor();
            Console.SetCursorPosition(GetYPosition(), GetXPosition());
            Console.Write(GetFruitIcon());
            Console.ForegroundColor = ConsoleColor.Gray;
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
    }

    public class Apple : Fruit
    {

        public Apple(int fieldSize) : base(fieldSize)
        {

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
}
