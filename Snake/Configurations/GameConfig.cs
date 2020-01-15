using System;

namespace Snake.Configurations
{
    public class GameConfig
    {
        public static string ScoresFile { get; private set; } = "scores.json";
        public static int CountColors { get; private set; } = 8;
        public static int CountSkins { get; private set; } = 8;
        public ConsoleColor[] ColorsSnake { get; private set; }
        public char[] SkinsSnake { get; private set; }

        public GameConfig()
        {
            ColorsSnake = new ConsoleColor[CountColors];
            ColorsSnake[0] = ConsoleColor.White;
            ColorsSnake[1] = ConsoleColor.Cyan;
            ColorsSnake[2] = ConsoleColor.Gray;
            ColorsSnake[3] = ConsoleColor.Red;
            ColorsSnake[4] = ConsoleColor.Yellow;
            ColorsSnake[5] = ConsoleColor.Green;
            ColorsSnake[6] = ConsoleColor.Blue;
            ColorsSnake[7] = ConsoleColor.Magenta;

            SkinsSnake = new char[CountSkins];
            SkinsSnake[0] = '@';
            SkinsSnake[1] = '#';
            SkinsSnake[2] = '$';
            SkinsSnake[3] = '%';
            SkinsSnake[4] = 'o';
            SkinsSnake[5] = 'm';
            SkinsSnake[6] = 'x';
            SkinsSnake[7] = 'a';
        }
    }
}
