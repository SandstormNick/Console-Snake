﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Snake
    {
        #region Properties
        private StateBase _state;
        private string SnakeHead { get; set; }
        private int XPosition { get; set; }
        private int YPosition { get; set; }

        private int PreviousYPosition { get; set; }
        private int PreviousXPosition { get; set; }

        private bool AddToBody { get; set; }

        List<string> SnakeBody { get; set; }
        List<int> SnakeBodyY { get; set; }
        List<int> SnakeBodyX { get; set; }
        static List<int> PreviousBodyXPositions { get; set; }
        static List<int> PreviousBodyYPositions { get; set; }
        private string SnakeDirection { get; set; }

        public StateBase State
        {
            get { return _state; }
            set { _state = value; }
        }
        #endregion

        public Snake(StateBase state)
        {
            SnakeHead = ">";
            XPosition = 1;
            YPosition = 1;
            PreviousXPosition = 1;
            PreviousYPosition = 1;
            AddToBody = false;
            SnakeDirection = "DOWN";

            SnakeBody = new List<string>();
            SnakeBodyY = new List<int>();
            SnakeBodyX = new List<int>();
            PreviousBodyXPositions = new List<int>();
            PreviousBodyYPositions = new List<int>();

            _state = state;
        }

        #region Methods
        //Method to set the State of the snake based on the key input
        public void Request()
        {
            _state.UpdatePreviousCoordinates(this);
            _state.UpdateSnakeDirection(this);
            _state.Handle(this);
            _state.UpdateSnakeHead(this);

            if (SnakeBody.Count > 0 && AddToBody == false)
            {
                SetPreviousBodyPositions();
                UpdateSnakeBodyPositions();
            }

            if (SnakeBody.Count > 0 && AddToBody == true)
            {
                SetPreviousBodyPositions();
                if (SnakeBodyX.Count > 0)
                {
                    UpdateSnakeBodyPositions();
                }
                AddSnakeBodyPositions();
                UpdateAddToBody();
            }
        }

        public void ReadKeyInput(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.W:
                    if (SnakeDirection != "DOWN")
                    {
                        _state = new ConcreteStateUp();
                    }
                    break;
                case ConsoleKey.S:
                    if (SnakeDirection != "UP")
                    {
                        _state = new ConcreteStateDown();
                    }
                    break;
                case ConsoleKey.D:
                    if (SnakeDirection != "LEFT")
                    {
                        _state = new ConcreteStateRight();
                    }
                    break;
                case ConsoleKey.A:
                    if (SnakeDirection != "RIGHT")
                    {
                        _state = new ConcreteStateLeft();
                    }
                    break;
            }
        }

        public void SetSnakeDirection(string direction)
        {
            SnakeDirection = direction;
        }

        public string GetSnakeHead()
        {
            return SnakeHead;
        }

        public void SetSnakeHead(string value)
        {
            SnakeHead = value;
        }

        public void UpdateAddToBody()
        {
            if (AddToBody == false)
            {
                AddToBody = true;
            }
            else
            {
                AddToBody = false;
            }
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

        public void SetPreviousBodyPositions()
        {
            PreviousBodyXPositions = new List<int>(SnakeBodyX);
            PreviousBodyYPositions = new List<int>(SnakeBodyY);
        }

        #region Snake Body
        public void AddSnakeBody()
        {
            SnakeBody.Add("*");
        }

        public int GetSnakeBodyCount()
        {
            return SnakeBody.Count;
        }

        public List<string> GetSnakeBody()
        {
            return SnakeBody;
        }

        public void AddSnakeBodyPositions()
        {
            if (SnakeBody.Count == 1)
            {
                SnakeBodyX.Add(PreviousXPosition);
                SnakeBodyY.Add(PreviousYPosition);
            }
            else
            {
                SnakeBodyX.Add(PreviousBodyXPositions.Last());
                SnakeBodyY.Add(PreviousBodyYPositions.Last());
            }
        }

        public void UpdateSnakeBodyPositions()
        {
            if (SnakeBody.Count > 0)
            {
                SnakeBodyX[0] = PreviousXPosition;
                SnakeBodyY[0] = PreviousYPosition;
            }

            for (int i = SnakeBodyX.Count() - 1; i > 0; i--)
            {
                SnakeBodyX[i] = PreviousBodyXPositions[i - 1];
                SnakeBodyY[i] = PreviousBodyYPositions[i - 1];
            }
        }

        public List<int> GetSnakeBodyX()
        {
            return SnakeBodyX;
        }

        public List<int> GetSnakeBodyY()
        {
            return SnakeBodyY;
        }

        public void RemoveSnakeBody()
        {
            for (int i = 0; i < SnakeBody.Count(); i++)
            {
                Console.SetCursorPosition(SnakeBodyY[i], SnakeBodyX[i]);
                Console.Write(" ");
            }
        }
        #endregion

        public bool DetectBodyCollision()
        {
            for (int i = 0; i < SnakeBody.Count(); i++)
            {
                if (XPosition == SnakeBodyX[i] && YPosition == SnakeBodyY[i])
                {
                    return false;
                }
            }

            return true;
        }

        public void DrawSnake()
        {
            Console.SetCursorPosition(GetPreviousYPosition(), GetPreviousXPosition());
            Console.Write(" ");

            Console.SetCursorPosition(GetYPosition(), GetXPosition());
            Console.Write(GetSnakeHead());

            for (int i = 0; i < PreviousBodyXPositions.Count(); i++)
            {
                Console.SetCursorPosition(PreviousBodyYPositions[i], PreviousBodyXPositions[i]);
                Console.Write(" ");
            }

            for (int i = 0; i < SnakeBody.Count(); i++)
            {
                Console.SetCursorPosition(SnakeBodyY[i], SnakeBodyX[i]);
                Console.Write(SnakeBody[i]);
            }
        }

        public void ResetSnake(ConcreteStateDown state, bool reset)
        {
            if (reset)
            {
                SnakeHead = ">";
                XPosition = 1;
                YPosition = 1;
                PreviousXPosition = 1;
                PreviousYPosition = 1;
                AddToBody = false;
                SnakeDirection = "DOWN";

                SnakeBody = new List<string>();
                SnakeBodyY = new List<int>();
                SnakeBodyX = new List<int>();
                PreviousBodyXPositions = new List<int>();
                PreviousBodyYPositions = new List<int>();

                _state = state;
            }
        }
        
        #endregion

    }

    #region State Classes
    public abstract class StateBase
    {
        public abstract void Handle(Snake snake);
        public abstract void UpdateSnakeHead(Snake snake);
        public abstract void UpdatePreviousCoordinates(Snake snake);
        public abstract void UpdateSnakeDirection(Snake snake);
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

        public override void UpdateSnakeDirection(Snake snake)
        {
            snake.SetSnakeDirection("DOWN");
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

        public override void UpdateSnakeDirection(Snake snake)
        {
            snake.SetSnakeDirection("UP");
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

        public override void UpdateSnakeDirection(Snake snake)
        {
            snake.SetSnakeDirection("RIGHT");
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

        public override void UpdateSnakeDirection(Snake snake)
        {
            snake.SetSnakeDirection("LEFT");
        }
    }
    #endregion
}
