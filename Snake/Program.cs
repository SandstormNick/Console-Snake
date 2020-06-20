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
            Game theGame = new Game(theField.GetFieldSize());
            Snake snake = new Snake(new ConcreteStateDown());

            theGame.DisplayWelcome();

            snake.Request();
            theField.SetField();
            theFruit.CreateFruit();
            //---------------------------------

            while (theGame.GetGameStatus() == true && cs.Key != ConsoleKey.Q)
            {
                if (theFruit.GetIsEaten())
                {
                    theGame.UpdateGameSpeed(theFruit.GetUpdateSpeed());
                    snake.AddSnakeBody();
                    snake.UpdateAddToBody();
                    theGame.UpdateBiteCount();
                    theGame.SetGameScore(theFruit.GetFruitPoints());
                    
                    if (theGame.GetBiteCount() % 5 == 0)
                    {
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
                theGame.DisplaySnakeBodyCount(snake.GetSnakeBodyCount()); //can be removed later

                while (Console.KeyAvailable == false && theGame.GetGameStatus() == true && theFruit.GetIsEaten() == false)
                {
                    Thread.Sleep(theGame.GetGameSpeed());

                    snake.Request();
                    theGame.SetGameStatus(theField.DetectWallCollisions(snake.GetXPosition(), snake.GetYPosition()), snake.DetectBodyCollision());
                    theFruit.DetectFruitCollision(snake.GetXPosition(), snake.GetYPosition());
                    theGame.DisplaySnakePosition(snake.GetXPosition(), snake.GetYPosition());
                
                    snake.DrawSnake();
                }
                if (!theFruit.GetIsEaten() && theGame.GetGameStatus())
                {
                    cs = Console.ReadKey(true);
                    snake.ReadKeyInput(cs);
                }

                if (!theGame.GetGameStatus())
                {
                    theGame.DisplayReplayMessage();
                    theGame.GetReplayResponse();
                    if (theGame.GetReset())
                    {
                        theFruit.RemoveOldFruit();
                        snake.RemoveSnakeBody();
                        theField.FixWall(snake.GetYPosition(), snake.GetXPosition());

                        snake.ResetSnake(new ConcreteStateDown(), theGame.GetReset());
                        theFruit = new Apple(theField.GetFieldSize());
                        theFruit.CreateFruit();
                    }
                }
                
            }
            theGame.DisplayEnd();
        }
    }

    //TO DO:
    //Freeze anymore adjustments...
    //Fruit:
    //-->(5) the more points heavy fruits should have a time limit on how long they can exist (nice to have)
    //-->(4) the fruit must not show up anywhere in the body or head of the snake
    //
    //Final:
    //--> (6) Refactor Code


    //FUTURE ADDITIONS:
    //--> add the ability for the snake to go through the walls (user decides this via input in the beginning)
}