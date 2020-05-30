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
            Apple appleFruit = new Apple(theField.GetFieldSize());
            Game theGame = new Game();
            Snake snake = new Snake(new ConcreteStateDown());
            snake.Request();
            theField.SetField();

            bool ifEaten = false; //temporary but make a plan so the fruit doesn't populate every iteration
            //---------------------------------

            while (theGame.GetGameStatus() == true && cs.Key != ConsoleKey.Q)
            {
                //temporary if statment
                if (!ifEaten)
                {
                    appleFruit.CreateFruit();
                    ifEaten = true;
                }
                
                while (Console.KeyAvailable == false)
                {
                    Thread.Sleep(300);

                    snake.Request();

                    snake.DrawSnake();

                }
                cs = Console.ReadKey(true);
                snake.ReadKeyInput(cs);
            }

            
        }
    }

    //TO DO:
    //Created the needed classes:
    // - Snake ->(Iterator pattern perhaps)
    // - Fruit ->gets placed somewhere randomly on the field
    // - Field -> th parameters that the snake can roam in
    // - Game -> keeps track of the game status - points, if snake is still alive, etc, snake speed
    //
    //Set collision methods
    // -> if the snake hits the walls
    // -> if the snake hits the fruit it should generate a new fruit and add some points to the total
}


//==================================================================================================
//Code to help get user input while the game loop is running - can delete at a later stage

//ConsoleKeyInfo cs = new ConsoleKeyInfo();
//do
//{
//    Console.WriteLine("\nPress a key to display; " + "press the 'P' key to quit.");
//    while (Console.KeyAvailable == false) Thread.Sleep(200);
//    cs = Console.ReadKey(true);
//    Console.WriteLine("You pressed the '{0}' key.", cs.Key);
//}
//while (cs.Key != ConsoleKey.Q);
