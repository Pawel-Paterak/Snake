using System;
using Snake.Controlers;
using System.Collections.Generic;
using Snake.Game.Enums;
using Snake.Configurations;
using Snake.Game.Managers;

namespace Snake.Game
{
    public class Snake
    {
        public List<GameObject> SnakeBody { get; private set; } = new List<GameObject>();
        public int Scores { get; private set; }
        public char SkinSnake { get; set; } = '@';
        public DifficultiGameEnum Difficulti { get; set; } = DifficultiGameEnum.Easy;
        public ConsoleColor ColorSnake { get; set; } = ConsoleColor.White;

        private Direction direction = Direction.Up;
        private Direction previousDirection = Direction.Up;

        public void Start(Vector2D startPoint)
        {
            SnakeBody.Add(new GameObject("Head", new Vector2D(startPoint.X, startPoint.Y), SkinSnake, ColorSnake, GameObjectTagEnum.Object, true));
            SnakeBody[0].Create();
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
            SnakeBody = new List<GameObject>();
            OnCloseKeyboard();
        }

        public bool Move()
        {
            Vector2D vecDirection = GetVectorDirection(direction);

            if (!CheckMovment())
                return false;

            if (CheckCollision(vecDirection))
                return false;

            CheckObjectInMyPosition();

            MoveSnakeBody();
            SnakeBody[0].Move(vecDirection, true, false, true) ;
            previousDirection = direction;
            return true;
        }

        private Vector2D GetVectorDirection(Direction direction)
        {
            Vector2D vectorDirection = new Vector2D(0, 0);
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

        private bool CheckMovment()
        {
            Vector2D previous = GetVectorDirection(previousDirection);
            Vector2D next =  GetVectorDirection(direction);
            if (!previous == next)
                return false;
            return true;
        }

        private bool CheckCollision(Vector2D direction)
        {
            GameManager gm = new GameManager();
            Vector2D position = SnakeBody[0].Position+direction;
            GameObject obj = gm.GetObject(position);
            return obj != null && obj.Collision && obj.Position == position;
        }

        private void CheckObjectInMyPosition()
        {
            GameManager gm = new GameManager();
            Vector2D position = SnakeBody[0].Position;
            GameObject obj = gm.GetObject(position, SnakeBody[0]);
            if (obj == null)
                return;

            switch (obj.Name)
            {
                case "Apple":
                    {
                        obj.Destroy();
                        AddSnakeBody();
                        if (Difficulti == DifficultiGameEnum.Easy)
                            Scores += 5;
                        else if (Difficulti == DifficultiGameEnum.Medium)
                            Scores += 10;
                        else
                            Scores += 15;
                        break;
                    }
            }

            switch (obj.Tag)
            {
                case GameObjectTagEnum.Teleport:
                    {
                        string name = obj.Name;
                        Teleport teleport = new Teleport();
                        GameObject objTeleport = gm.FindObject(teleport.GetNameTeleportTo(name));
                        if (objTeleport != null)
                            SnakeBody[0].Move(objTeleport.Position, false);
                        break;
                    }
            }
        }

        private void AddSnakeBody()
        {
            Vector2D position = SnakeBody[SnakeBody.Count - 1].Position;
            SnakeBody.Add(new GameObject("Body"+(SnakeBody.Count-1), position, SkinSnake, ColorSnake, GameObjectTagEnum.Object, true));
            SnakeBody[SnakeBody.Count-1].Create();
        }

        private void MoveSnakeBody()
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
