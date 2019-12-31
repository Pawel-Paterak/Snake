using Snake.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Game.Render
{
    public class ConsoleRender
    {
        public void Clear()
           => Console.Clear();

        public void SetCursor(int x, int y)
        {
            ConsoleConfiguration consoleConfig = new ConsoleConfiguration();
            if (x >= 0 && x < consoleConfig.widht && y >= 0 && y < consoleConfig.height)
                Console.SetCursorPosition(x, y);
        }

        public void Write(string chars)
         => Console.Write(chars);

        public void Write(string chars, int x, int y)
        {
            SetCursor(x, y);
            Console.Write(chars);
        }
    }
}
