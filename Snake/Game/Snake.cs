using System;
using Snake.Controlers;
using System.Collections.Generic;
using Snake.Game.Enums;
using Snake.Configurations;

namespace Snake.Game
{
    public class Snake
    {
        public List<Object> SnakeBody { get; private set; } = new List<Object>();
        public int Scores { get; private set; }
        public char SkinSnake { get; set; } = '@';
        public DifficultiGameEnum Difficulti { get; set; } = DifficultiGameEnum.Easy;
        public ConsoleColor ColorSnake { get; set; } = ConsoleColor.White;

        private Direction direction = Direction.Up;
        private Direction previousDirection = Direction.Up;

        public void Start(Vector2D startPoint)
        {
            ConsoleConfig config = new ConsoleConfig();
            SnakeBody.Add(new Object("Head", new Vector2D(startPoint.X, startPoint.Y), SkinSnake, ColorSnake, true));
            KeyboardControl.PressKeyEvent += OnPressKey;
            KeyboardControl.KeyboardCloseEvent += OnCloseKeyboard;
        }
        public void Close()
        {
            Scores = 0;
            direction = Direction.Up;
            previousDirection = Direction.Up;
            Difficulti = DifficultiGameEnum.Easy;
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

            if (VeryficationCollision(vecDirection))
                return false;

            MoveBody();
            SnakeBody[0].Move(vecDirection, true, false, true) ;
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
            Vector2D previous = VectorDirection(previousDirection);
            Vector2D next =  VectorDirection(direction);
            if (!previous == next)
                return false;
            return true;
        }
        private bool VeryficationCollision(Vector2D direction)
        {
            GameManager gm = new GameManager();
            Object obj = gm.GetObject(SnakeBody[0].Position+ direction);
            if (obj == null)
                return false;

            if (obj.Collision)
                return true;
            else
            {
                switch(obj.Name)
                {
                    case "Apple":
                        {
                            obj.Destroy();
                            AddBody();
                            if (Difficulti == DifficultiGameEnum.Easy)
                                Scores += 5;
                            else if (Difficulti == DifficultiGameEnum.Medium)
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
            Vector2D position = SnakeBody[SnakeBody.Count - 1].Position;
            SnakeBody.Add(new Object("Body"+(SnakeBody.Count-1), position, SkinSnake, ColorSnake, true));
        }
        private void MoveBody()
        {
            SnakeBody[SnakeBody.Count - 1].ClearRender();
            for (int i = SnakeBody.Count - 1; i > 0; i--)
                if (SnakeBody[i] != null && SnakeBody[i - 1] != null)
                    SnakeBody[i].Move(SnakeBody[i - 1].Position, true, false, false);
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
