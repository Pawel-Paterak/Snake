using Snake.Controlers;
using System.Collections.Generic;

namespace Snake.Game
{
    public class Snake
    {
        public List<Object> objs = new List<Object>();

        private Direction direction = Direction.Up;
        private Direction previousDirection = Direction.Up;
        private KeyboardControl Keyboard { get; } = new KeyboardControl();

        public void Start()
        {
            objs.Add(new Object("Head", new Vector2D(20, 20), '@', true));
            Keyboard.Start();
            Keyboard.PressKeyEvent += OnPressKey;
            Keyboard.KeyboardCloseEvent += OnCloseKeyboard;
        }

        public void Close()
        {
            for(int i=0; i<objs.Count; i++)
                objs[i].Destroy();
            OnCloseKeyboard(true);
            Keyboard.Close();
        }

        public bool Move()
        {
            if ((previousDirection == Direction.Up && direction == Direction.Down) ||
                    (previousDirection == Direction.Down && direction == Direction.Up) ||
                    (previousDirection == Direction.Left && direction == Direction.Right) ||
                    (previousDirection == Direction.Right && direction == Direction.Left))
            {
                return false;
            }

            switch (direction)
            {
                case Direction.Up:
                    objs[0].position.y--;
                    break;
                case Direction.Down:
                    objs[0].position.y++;
                    break;
                case Direction.Left:
                    objs[0].position.x--;
                    break;
                case Direction.Right:
                    objs[0].position.x++;
                    break;
            }
            previousDirection = direction;
            return true;
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
