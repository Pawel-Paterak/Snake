using Snake.Configurations;

namespace Snake
{
    static class Program
    {
        static void Main(string[] args)
        {
            ConsoleConfig consoleConfiguration = new ConsoleConfig();
            consoleConfiguration.Configuration();

            Core core = new Core();
            core.Start();
        }
    }
}
