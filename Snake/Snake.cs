using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Snake
    {
        private StateBase _state;
        private string SnakeHead { get; set; }
        private int XPosition { get; set; }
        private int YPosition { get; set; }

        private int PreviousYPosition { get; set; }
        private int PreviousXPosition { get; set; }

        private int LastBodyXPosition { get; set; }
        private int LastBodyYPosition { get; set; }

        List<string> SnakeBody { get; set; }
        List<int> SnakeBodyY { get; set; }
        List<int> SnakeBodyX { get; set; }

        public Snake(StateBase state)
        {
            SnakeHead = ">";
            XPosition = 1;
            YPosition = 1;
            PreviousXPosition = 1;
            PreviousYPosition = 1;
            LastBodyXPosition = 0;
            LastBodyYPosition = 0;

            SnakeBody = new List<string>();
            SnakeBodyY = new List<int>();
            SnakeBodyX = new List<int>();

            _state = state;
        }

        public StateBase State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void Request()
        {
            _state.UpdatePreviousCoordinates(this);
            _state.Handle(this);
            _state.UpdateSnakeHead(this);
            SetLastBodyCoordintates();
        }

        public void ReadKeyInput(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.W:
                    _state = new ConcreteStateUp();
                    break;
                case ConsoleKey.S:
                    _state = new ConcreteStateDown();
                    break;
                case ConsoleKey.D:
                    _state = new ConcreteStateRight();
                    break;
                case ConsoleKey.A:
                    _state = new ConcreteStateLeft();
                    break;
            }
        }

        public void DrawSnake()
        {
            Console.SetCursorPosition(GetPreviousYPosition(), GetPreviousXPosition());
            Console.Write(" ");

            Console.SetCursorPosition(GetYPosition(), GetXPosition());
            Console.Write(GetSnakeHead());

            for (int i = 0; i < SnakeBody.Count(); i++)
            {
                Console.SetCursorPosition(SnakeBodyY[i], SnakeBodyX[i]);
                Console.Write(SnakeBody[i]);
            }

            if (SnakeBody.Count > 0)
            {
                Console.SetCursorPosition(LastBodyYPosition, LastBodyXPosition);
                Console.Write(" ");
            }
            
        }

        public void SetSnakeHead(string value)
        {
            SnakeHead = value;
        }

        #region Get X and Y Coordinates
        public int GetYPosition()
        {
            return YPosition;
        }

        public int GetXPosition()
        {
            return XPosition;
        }

        public int GetPreviousYPosition()
        {
            return PreviousYPosition;
        }

        public int GetPreviousXPosition()
        {
            return PreviousXPosition;
        }
        #endregion

        public string GetSnakeHead()
        {
            return SnakeHead;
        }

        #region Move Coordinates
        public void MoveDown()
        {
            XPosition++;
        }

        public void MoveUp()
        {
            XPosition--;
        }

        public void MoveRight()
        {
            YPosition++;
        }

        public void MoveLeft()
        {
            YPosition--;
        }
        #endregion

        public void SetPreviousPositions(int currentX, int currentY)
        {
            PreviousXPosition = currentX;
            PreviousYPosition = currentY;
        }

        public void UpdateSnakeBody()
        {
            SnakeBody.Add("*");
        }

        public List<string> GetSnakeBody()
        {
            return SnakeBody;
        }

        public void AddSnakeBodyPositions()
        {
            SnakeBodyX.Add(0);
            SnakeBodyY.Add(0);
        }

        public void UpdateSnakeBodyPositions()
        {
            int tempX = 0;
            int tempY = 0;

            if (SnakeBody.Count > 0)
            {
                tempX = SnakeBodyX[0];
                tempY = SnakeBodyY[0];
                SnakeBodyX[0] = PreviousXPosition;
                SnakeBodyY[0] = PreviousYPosition;
            }

            for (int i = 1; i < SnakeBody.Count(); i++)
            {
                SnakeBodyX[i] = tempX;
                SnakeBodyY[i] = tempY;

                tempX = SnakeBodyY[i];
                tempY = SnakeBodyY[i];
            }
        }

        public void SetLastBodyCoordintates()
        {
            if (SnakeBody.Count > 0)
            {
                LastBodyXPosition = SnakeBodyX.Last();
                LastBodyYPosition = SnakeBodyY.Last();
            }
        }
    }

    #region State Classes
    public abstract class StateBase
    {
        public abstract void Handle(Snake snake);
        public abstract void UpdateSnakeHead(Snake snake);
        public abstract void UpdatePreviousCoordinates(Snake snake);
    }

    public class ConcreteStateDown : StateBase
    {
        public override void Handle(Snake snake)
        {
            snake.State = this;
            snake.MoveDown();
            
        }

        public override void UpdatePreviousCoordinates(Snake snake)
        {
            snake.SetPreviousPositions(snake.GetXPosition(), snake.GetYPosition());
        }

        public override void UpdateSnakeHead(Snake snake)
        {
            snake.SetSnakeHead("V");
        }

    }

    public class ConcreteStateUp : StateBase
    {
        public override void Handle(Snake snake)
        {
            snake.State = this;
            snake.MoveUp();
        }

        public override void UpdatePreviousCoordinates(Snake snake)
        {
            snake.SetPreviousPositions(snake.GetXPosition(), snake.GetYPosition());
        }

        public override void UpdateSnakeHead(Snake snake)
        {
            snake.SetSnakeHead("^");
        }
    }

    public class ConcreteStateRight : StateBase
    {
        public override void Handle(Snake snake)
        {
            snake.State = this;
            snake.MoveRight();
        }

        public override void UpdatePreviousCoordinates(Snake snake)
        {
            snake.SetPreviousPositions(snake.GetXPosition(), snake.GetYPosition());
        }

        public override void UpdateSnakeHead(Snake snake)
        {
            snake.SetSnakeHead(">");
        }
    }

    public class ConcreteStateLeft : StateBase
    {
        public override void Handle(Snake snake)
        {
            snake.State = this;
            snake.MoveLeft();
        }

        public override void UpdatePreviousCoordinates(Snake snake)
        {
            snake.SetPreviousPositions(snake.GetXPosition(), snake.GetYPosition());
        }

        public override void UpdateSnakeHead(Snake snake)
        {
            snake.SetSnakeHead("<");
        }
    }
    #endregion
}
