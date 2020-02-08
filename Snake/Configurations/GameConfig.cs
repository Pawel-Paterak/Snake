using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Enums;
using System;
using System.Collections.Generic;

namespace Snake.Configurations
{
    public class GameConfig
    {
        public static string ScoresFile { get; private set; } = "scores.json";
        public int CountColors { get => ColorsSnake.Length; }
        public int CountDifficulti { get => Difficulti.Length; }
        public int CountSkins { get => SkinsSnake.Length;  }
        public int CountMap { get => Maps.Length;}
        public ConsoleColor[] ColorsSnake { get; private set; }
        public DifficultiGameEnum[] Difficulti { get; private set; }
        public char[] SkinsSnake { get; private set; }
        public MapFile[] Maps { get; private set; }

        private string PathMaps { get; set; } = "Maps/";

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
            ColorsSnake = new ConsoleColor[8];
            ColorsSnake[0] = ConsoleColor.White;
            ColorsSnake[1] = ConsoleColor.Cyan;
            ColorsSnake[2] = ConsoleColor.Gray;
            ColorsSnake[3] = ConsoleColor.Red;
            ColorsSnake[4] = ConsoleColor.Yellow;
            ColorsSnake[5] = ConsoleColor.Green;
            ColorsSnake[6] = ConsoleColor.Blue;
            ColorsSnake[7] = ConsoleColor.Magenta;
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
            SkinsSnake = new char[8];
            SkinsSnake[0] = '@';
            SkinsSnake[1] = '#';
            SkinsSnake[2] = '$';
            SkinsSnake[3] = '%';
            SkinsSnake[4] = 'o';
            SkinsSnake[5] = 'm';
            SkinsSnake[6] = 'x';
            SkinsSnake[7] = 'a';
        }

        private void InitializeMaps()
        {
            JsonManager jsonManager = new JsonManager();
            if (!jsonManager.DirectoryExists(PathMaps))
                return;
            List<MapFile> maps = new List<MapFile>();
            string[] jsonFiles = jsonManager.GetJsonFiles(PathMaps);
            foreach(string file in jsonFiles)
            {
                MapFile map = jsonManager.Read<MapFile>(file);
                if (map != null)
                    maps.Add(map);
            }
            Maps = maps.ToArray();
        }

    }
}
