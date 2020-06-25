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
            ConsoleKeyInfo cki = new ConsoleKeyInfo();

            Field theField = new Field();
            Fruit theFruit = new Apple(theField.GetFieldSize());
            Game theGame = new Game(theField.GetFieldSize());
            Snake snake = new Snake(new ConcreteStateDown());

            theGame.DisplayWelcome();

            snake.Request();
            theField.SetField();
            theFruit.CreateFruit(snake.GetXPosition(), snake.GetYPosition(), snake.GetSnakeBodyX(), snake.GetSnakeBodyY());

            //main game play loop
            while (theGame.GetGameStatus() == true && cki.Key != ConsoleKey.Q) //Q can quit while game is in play
            {
                //if the Fruit has been eaten access this if block
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

                    theFruit.CreateFruit(snake.GetXPosition(), snake.GetYPosition(), snake.GetSnakeBodyX(), snake.GetSnakeBodyY());
                    theFruit.SetIsEaten(false);
                }

                //meta data
                theGame.DisplayGameScore();
                theGame.DisplaySnakeBodyCount(snake.GetSnakeBodyCount());

                //this while loop runs while a key HAS not been pressed &&
                //the game status is still true meaning the snake has not died &&
                //the fruit has not been eaten yet
                while (Console.KeyAvailable == false && theGame.GetGameStatus() == true && theFruit.GetIsEaten() == false)
                {
                    //the game sleeps depending on the game speed
                    //this is what produces the speed of the snake as it gets faster
                    Thread.Sleep(theGame.GetGameSpeed());

                    snake.Request();
                    theGame.SetGameStatus(theField.DetectWallCollisions(snake.GetXPosition(), snake.GetYPosition()), snake.DetectBodyCollision());
                    theFruit.DetectFruitCollision(snake.GetXPosition(), snake.GetYPosition());
                    theGame.DisplaySnakePosition(snake.GetXPosition(), snake.GetYPosition());

                    //The Berry fruit has a time limit on it while the other fruits do not
                    //therefore if it is a Berrt run this if block
                    if (theFruit.GetIsBerry())
                    {
                        theFruit.UpdateBerryMoves();
                        if (theFruit.GetBerryMoves() <= 0)
                        {
                            theFruit.RemoveOldFruit();
                            theFruit = new Apple(theField.GetFieldSize());
                            theFruit.CreateFruit(snake.GetXPosition(), snake.GetYPosition(), snake.GetSnakeBodyX(), snake.GetSnakeBodyY());
                        }
                    }

                    //with each iteration in this loop the snake must be drawn
                    snake.DrawSnake();
                }
                
                //when the while loop is exited due to a key press, that key needs to be read
                if (!theFruit.GetIsEaten() && theGame.GetGameStatus())
                {
                    cki = Console.ReadKey(true);
                    snake.ReadKeyInput(cki);
                }

                //if the Game Status is false meaning the snake has died
                //give the user the choice of replaying
                //if they choose to the game is reset
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
                        theFruit.CreateFruit(snake.GetXPosition(), snake.GetYPosition(), snake.GetSnakeBodyX(), snake.GetSnakeBodyY());
                    }
                }
                
            }
            theGame.DisplayEnd();
        }
    }

    //FUTURE ADDITIONS:
    //Write score to file and keep track of high score, - notepad doc
    //allow user to enter a player name, this can be written with the high score to the doc
    //--> add the ability for the snake to go through the walls (user decides this via input in the beginning)
}