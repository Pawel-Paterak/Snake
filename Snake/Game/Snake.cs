using System;
using Snake.Controlers;
using System.Collections.Generic;

namespace Snake.Game
{
    public class Snake
    {
        public List<Object> SnakeBody { get; private set; } = new List<Object>();
        public int Scores { get; private set; }

        private Direction direction = Direction.Up;
        private Direction previousDirection = Direction.Up;

        public void Start()
        {
            SnakeBody.Add(new Object("Head", new Vector2D(20, 20), '@', ConsoleColor.White, true));
            KeyboardControl.Start();
            KeyboardControl.PressKeyEvent += OnPressKey;
            KeyboardControl.KeyboardCloseEvent += OnCloseKeyboard;
        }

        public void Close()
        {
            for(int i=0; i<SnakeBody.Count; i++)
                SnakeBody[i].Destroy();
            OnCloseKeyboard();
        }

        public bool Move()
        {
            Vector2D vecDirection = VectorDirection(direction);

            if (!VeryficationDirection())
                return false;

            if (VeryficationDirectionCollision(vecDirection))
                return false;

            MoveBody();
            SnakeBody[0].position += vecDirection;
            previousDirection = direction;
            return true;
        }

        private Vector2D VectorDirection(Direction direction)
        {
            Vector2D vectorDirection = new Vector2D();
            switch (direction)
            {
                case Direction.Up:
                    vectorDirection.y--;
                    break;
                case Direction.Down:
                    vectorDirection.y++;
                    break;
                case Direction.Left:
                    vectorDirection.x--;
                    break;
                case Direction.Right:
                    vectorDirection.x++;
                    break;
            }
            return vectorDirection;
        }

        private bool VeryficationDirection()
        {
            if ((previousDirection == Direction.Up && direction == Direction.Down) ||
                   (previousDirection == Direction.Down && direction == Direction.Up) ||
                   (previousDirection == Direction.Left && direction == Direction.Right) ||
                   (previousDirection == Direction.Right && direction == Direction.Left))
            {
                return false;
            }
            return true;
        }

        private bool VeryficationDirectionCollision(Vector2D direction)
        {
            Object obj = GameManager.GetObject(SnakeBody[0].position+ direction);
            if (obj != null && obj.collision)
                return true;

            if(obj != null && !obj.collision)
            {
                switch(obj.name)
                {
                    case "apple":
                        {
                            obj.Destroy();
                            AddBody();
                            Scores += 10;
                            break;
                        }
                }
            }

            return false;
        }

        private void AddBody()
        {
            SnakeBody.Add(new Object("Body"+(SnakeBody.Count-1), new Vector2D(20, 20), '@', ConsoleColor.White, true));
        }

        private void MoveBody()
        {
            for (int i = SnakeBody.Count - 1; i > 0; i--)
                if (SnakeBody[i] != null && SnakeBody[i - 1] != null)
                    SnakeBody[i].position = SnakeBody[i - 1].position;
        }

        private void OnPressKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    direction = Direction.Up;
                    break;
                case ConsoleKey.S:
                    direction = Direction.Down;
                    break;
                case ConsoleKey.A:
                    direction = Direction.Left;
                    break;
                case ConsoleKey.D:
                    direction = Direction.Right;
                    break;
            }
        }

        private void OnCloseKeyboard()
        {
            KeyboardControl.PressKeyEvent -= OnPressKey;
            KeyboardControl.KeyboardCloseEvent -= OnCloseKeyboard;
        }
    }
}
