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

        private int XPosition { get; set; }
        private int YPosition { get; set; }
        private int FieldSize { get; set; }

        public Fruit(int fieldSize)
        {
            FieldSize = fieldSize;
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
}
