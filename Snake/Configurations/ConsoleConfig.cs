using System;

namespace Snake.Configurations
{
    public class ConsoleConfig
    {
        public int Widht {get; private set;} = 40;
        public int Height { get; private set; } = 40;

        private const string title = "Snake";
        private const bool cursorVisible = false;

        public void Configuration()
        {
            WindowTitle(title);
            WindowsSize(Widht, Height);
            CursorVisible(cursorVisible);
        }

        private void WindowTitle(string title)
          => Console.Title = title;
        private void WindowsSize(int widht, int height)
        {
            Console.SetWindowSize(widht, height);
            Console.SetBufferSize(widht, height);
        }
        private void CursorVisible(bool visible)
            => Console.CursorVisible = visible;
    }
}
