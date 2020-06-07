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
            //---------------------------------

            while (theGame.GetGameStatus() == true && cs.Key != ConsoleKey.Q)
            {
                if (theFruit.GetIsEaten())
                {
                    theGame.UpdateBiteCount();
                    theGame.SetGameScore(theFruit.GetFruitPoints());
                    
                    if (theGame.GetBiteCount() % 5 == 0)
                    {
                        theGame.UpdateGameSpeed();
                        theFruit = new Berry(theField.GetFieldSize());
                    }
                    else if (theGame.GetBiteCount() % 3 == 0)
                    {
                        theFruit = new Banana(theField.GetFieldSize());
                    }
                    else
                    {
                        theFruit = new Apple(theField.GetFieldSize());
                    }

                    theFruit.CreateFruit();
                    theFruit.SetIsEaten(false);
                }

                theGame.DisplayGameScore();

                while (Console.KeyAvailable == false && theGame.GetGameStatus() == true && theFruit.GetIsEaten() == false)
                {
                    Thread.Sleep(theGame.GetGameSpeed());

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
    //----> the more points heavy fruits should have a time limit on how long they can exist (nice to have) 
    //
    //Game:
    //----> Welcome and End Screens (nice to have)
    //--> Update the speed only when berry is eaten
    //
    //Snake:
    //-->Grow the snake (use arrays/lists to keep track of each part of the snake)
}