using Snake.Controlers;
using System.Collections.Generic;

namespace Snake.Game
{
    public class Snake
    {
        public List<Object> snakeBody = new List<Object>();

        private Direction direction = Direction.Up;
        private Direction previousDirection = Direction.Up;
        private KeyboardControl Keyboard { get; } = new KeyboardControl();

        public void Start()
        {
            snakeBody.Add(new Object("Head", new Vector2D(20, 20), '@', true));
            Keyboard.Start();
            Keyboard.PressKeyEvent += OnPressKey;
            Keyboard.KeyboardCloseEvent += OnCloseKeyboard;
        }

        public void Close()
        {
            for(int i=0; i<snakeBody.Count; i++)
                snakeBody[i].Destroy();
            OnCloseKeyboard(true);
            Keyboard.Close();
        }

        public bool Move()
        {
            Vector2D vecDirection = VectorDirection(direction);

            if (!VeryficationDirection())
                return false;

            if (VeryficationDirectionCollision(vecDirection))
                return false;

            snakeBody[0].position += vecDirection;
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
            Object obj = GameManager.GetObject(snakeBody[0].position+ direction);
            if (obj != null && obj.collision)
                return true;
            return false;
        }

        private void OnPressKey(char key)
        {
            switch (key)
            {
                case 'w':
                    direction = Direction.Up;
                    break;
                case 's':
                    direction = Direction.Down;
                    break;
                case 'a':
                    direction = Direction.Left;
                    break;
                case 'd':
                    direction = Direction.Right;
                    break;
            }
        }

        private void OnCloseKeyboard(bool closing)
        {
            Keyboard.PressKeyEvent -= OnPressKey;
            Keyboard.KeyboardCloseEvent -= OnCloseKeyboard;
        }
    }
}
