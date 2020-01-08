using Snake.Configurations;
using System;

namespace Snake.Game.Render
{
    public class ConsoleRender
    {
        public void Clear()
           => Console.Clear();

        public void SetCursor(int x, int y)
        {
            ConsoleConfig consoleConfig = new ConsoleConfig();
            if (x >= 0 && x < consoleConfig.Widht && y >= 0 && y < consoleConfig.Height)
                Console.SetCursorPosition(x, y);
        }

        public void Write(string chars)
         => Console.Write(chars);

        public void Write(string chars, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(chars);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Write(string chars, int x, int y)
        {
            SetCursor(x, y);
            Console.Write(chars);
        }

        public void Write(string chars, ConsoleColor color, int x, int y)
        {
            SetCursor(x, y);
            Console.ForegroundColor = color;
            Console.Write(chars);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
