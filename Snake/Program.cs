using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            //---------------------------------

            ConsoleKeyInfo cs = new ConsoleKeyInfo();

            //---------------------------------
            //Need to neaten this area up a bit

            Field theField = new Field();
            Fruit theFruit = new Apple(theField.GetFieldSize());
            Game theGame = new Game();
            Snake snake = new Snake(new ConcreteStateDown());
            snake.Request();
            theField.SetField();
            theFruit.CreateFruit();

            //bool ifEaten = false; //temporary but make a plan so the fruit doesn't populate every iteration
            //---------------------------------

            while (theGame.GetGameStatus() == true && cs.Key != ConsoleKey.Q)
            {
                if (theFruit.GetIsEaten())
                {
                    theGame.SetGameScore(theFruit.GetFruitPoints());
                    theFruit.CreateFruit();
                    theFruit.SetIsEaten(false);
                }

                theGame.DisplayGameScore();

                while (Console.KeyAvailable == false && theGame.GetGameStatus() == true && theFruit.GetIsEaten() == false)
                {
                    Thread.Sleep(300);

                    snake.Request();
                    theGame.SetGameStatus(theField.DetectWallCollisions(snake.GetXPosition(), snake.GetYPosition()));
                    theFruit.DetectFruitCollision(snake.GetXPosition(), snake.GetYPosition());
                    theGame.DisplaySnakePosition(snake.GetXPosition(), snake.GetYPosition());

                    snake.DrawSnake();

                }
                if (!theFruit.GetIsEaten())
                {
                    cs = Console.ReadKey(true);
                    snake.ReadKeyInput(cs);
                }
                
            }

            
        }
    }

    //TO DO:
    //Fruit:
    //-->Generate some different fruit (say 2 more)
    //--> Create an algorithm to switch between the fruits
    //----> the more points heavy fruits should have a time limit on how long they can exist (nice to have) 
    //
    //Game:
    //----> Welcome and End Screens (nice to have)
    //--> Introduce a game speed that gets progressively faster with each fruit eaten
    //
    //Snake:
    //-->Grow the snake (use arrays to keep track of each part of the snake)
}