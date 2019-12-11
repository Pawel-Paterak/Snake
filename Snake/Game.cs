using Snake.Controlers;
using System;
using System.Threading;

namespace Snake
{
    public class Game
    {
        private Direction direction = Direction.Up;
        private KeyboardControl Keyboard { get; } = new KeyboardControl();

        public void Start()
        {
            Configuration();
            Keyboard.Close();
        }

        private void Configuration()
        {
            Keyboard.Start();
            Keyboard.PressKeyEvent += OnPressKey;
            Keyboard.KeyboardCloseEvent += OnCloseKeyboard;
        }

        private void OnPressKey(char key)
        {
            switch(key)
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
