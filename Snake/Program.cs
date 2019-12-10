using Snake.Configurations;
using System;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleConfiguration consoleConfiguration = new ConsoleConfiguration();
            consoleConfiguration.Configuration();
            Core core = new Core();
            core.Start();
        }
    }
}
