using Snake.Controlers;

namespace Snake.Game
{
    public class Snake
    {
        public int HeadPosX { get; private set; } = 20;
        public int HeadPosY { get; private set; } = 20;

        private Direction direction = Direction.Up;
        private Direction previousDirection = Direction.Up;
        private KeyboardControl Keyboard { get; } = new KeyboardControl();

        public void Start()
        {
            Keyboard.Start();
            Keyboard.PressKeyEvent += OnPressKey;
            Keyboard.KeyboardCloseEvent += OnCloseKeyboard;
        }

        public void Close()
        {
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
                    HeadPosY--;
                    break;
                case Direction.Down:
                    HeadPosY++;
                    break;
                case Direction.Left:
                    HeadPosX--;
                    break;
                case Direction.Right:
                    HeadPosX++;
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
