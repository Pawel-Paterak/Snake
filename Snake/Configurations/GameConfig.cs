using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Enums;
using Snake.Game.Menu;
using System;
using System.Collections.Generic;

namespace Snake.Configurations
{
    public class GameConfig
    {
        public static string ScoresFile { get; private set; } = "scores.json";
        public static string PathMaps { get; set; } = "Maps/";
        public ConsoleColor[] Colors { get; private set; }
        public DifficultiGameEnum[] Difficulti { get; private set; }
        public char[] Skins { get; private set; }
        public MapFile[] Maps { get; private set; }

        public GameConfig()
        {
            Initialize();
        }

        public void Initialize()
        {
            InitializeColors();
            InitializeDifficulti();
            InitializeSkins();
            InitializeMaps();
        }

        private void InitializeColors()
        {
            Colors = new ConsoleColor[8];
            Colors[0] = ConsoleColor.White;
            Colors[1] = ConsoleColor.Cyan;
            Colors[2] = ConsoleColor.Gray;
            Colors[3] = ConsoleColor.Red;
            Colors[4] = ConsoleColor.Yellow;
            Colors[5] = ConsoleColor.Green;
            Colors[6] = ConsoleColor.Blue;
            Colors[7] = ConsoleColor.Magenta;
        }

        private void InitializeDifficulti()
        {
            Difficulti = new DifficultiGameEnum[3];
            Difficulti[0] = DifficultiGameEnum.Easy;
            Difficulti[1] = DifficultiGameEnum.Medium;
            Difficulti[2] = DifficultiGameEnum.Hard;
        }

        private void InitializeSkins()
        {
            Skins = new char[8];
            Skins[0] = '@';
            Skins[1] = '#';
            Skins[2] = '$';
            Skins[3] = '%';
            Skins[4] = 'o';
            Skins[5] = 'm';
            Skins[6] = 'x';
            Skins[7] = 'a';
        }

        private void InitializeMaps()
        {
            FileManager fileManager = new FileManager();
            Maps = fileManager.GetMaps();
        }
    }
}
