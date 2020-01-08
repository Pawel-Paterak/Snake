using System;
using Snake.Controlers;
using System.Collections.Generic;
using Snake.Game.Enums;

namespace Snake.Game
{
    public class Snake
    {
        public List<Object> SnakeBody { get; private set; } = new List<Object>();
        public int Scores { get; private set; }
        public DifficultiGame Difficulti { get; set; } = DifficultiGame.Easy;
        public ConsoleColor ColorSnake { get; set; } = ConsoleColor.White;
        public char SkinSnake { get; set; } = '@';

        private Direction direction = Direction.Up;
        private Direction previousDirection = Direction.Up;

        public void Start()
        {
            SnakeBody.Add(new Object("Head", new Vector2D(20, 20), SkinSnake, ColorSnake, true));
            KeyboardControl.PressKeyEvent += OnPressKey;
            KeyboardControl.KeyboardCloseEvent += OnCloseKeyboard;
        }

        public void Close()
        {
            Scores = 0;
            direction = Direction.Up;
            previousDirection = Direction.Up;
            Difficulti = DifficultiGame.Easy;
            ColorSnake = ConsoleColor.White;
            SkinSnake = '@';
            for (int i=0; i<SnakeBody.Count; i++)
                SnakeBody[i].Destroy();
            SnakeBody = new List<Object>();
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
            SnakeBody[0].Position += vecDirection;
            previousDirection = direction;
            return true;
        }

        private Vector2D VectorDirection(Direction direction)
        {
            Vector2D vectorDirection = new Vector2D();
            switch (direction)
            {
                case Direction.Up:
                    vectorDirection.Y--;
                    break;
                case Direction.Down:
                    vectorDirection.Y++;
                    break;
                case Direction.Left:
                    vectorDirection.X--;
                    break;
                case Direction.Right:
                    vectorDirection.X++;
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
            Object obj = GameManager.GetObject(SnakeBody[0].Position+ direction);
            if (obj != null && obj.Collision)
                return true;

            if(obj != null && !obj.Collision)
            {
                switch(obj.Name)
                {
                    case "apple":
                        {
                            obj.Destroy();
                            AddBody();
                            if (Difficulti == DifficultiGame.Easy)
                                Scores += 5;
                            else if (Difficulti == DifficultiGame.Medium)
                                Scores += 10;
                            else
                                Scores += 15;
                            break;
                        }
                }
            }

            return false;
        }

        private void AddBody()
        {
            SnakeBody.Add(new Object("Body"+(SnakeBody.Count-1), new Vector2D(20, 20), SkinSnake, ColorSnake, true));
        }

        private void MoveBody()
        {
            for (int i = SnakeBody.Count - 1; i > 0; i--)
                if (SnakeBody[i] != null && SnakeBody[i - 1] != null)
                    SnakeBody[i].Position = SnakeBody[i - 1].Position;
        }

        private void OnPressKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey nullKey when key == ConsoleKey.W || key == ConsoleKey.UpArrow:
                    direction = Direction.Up;
                    break;
                case ConsoleKey nullKey when key == ConsoleKey.S || key == ConsoleKey.DownArrow:
                    direction = Direction.Down;
                    break;
                case ConsoleKey nullKey when key == ConsoleKey.A || key == ConsoleKey.LeftArrow:
                    direction = Direction.Left;
                    break;
                case ConsoleKey nullKey when key == ConsoleKey.D || key == ConsoleKey.RightArrow:
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
