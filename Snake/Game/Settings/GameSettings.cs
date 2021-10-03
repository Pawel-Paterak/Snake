using Snake.Configurations;
using Snake.Files;
using Snake.Game.Enums;
using Snake.Game.Menu;
using System;

namespace Snake.Game.Settings
{
    class GameSettings
    {
        public static string PlayerName { get; set; }

        public int GetNumberColor()
        => MenuManager.Singleton.AdvencedOptions[CanvasEnum.CustomsSnake][0].Intiger;

        public int GetNumberSkin()
         => MenuManager.Singleton.AdvencedOptions[CanvasEnum.CustomsSnake][1].Intiger;

        public int GetNumberMap()
         => MenuManager.Singleton.AdvencedOptions[CanvasEnum.GameSettings][0].Intiger;

        public int GetNumberDifficulti()
         => MenuManager.Singleton.AdvencedOptions[CanvasEnum.GameSettings][1].Intiger;

        public ConsoleColor GetSelectedColor()
        {
            GameConfig config = new GameConfig();
            return config.Colors[GetNumberColor()];
        }

        public DifficultiGameEnum GetSelectedDifficulti()
        {
            GameConfig config = new GameConfig();
            return config.Difficulti[GetNumberDifficulti()];
        }

        public char GetSelectedSkin()
        {
            GameConfig config = new GameConfig();
            return config.Skins[GetNumberSkin()];
        }

        public MapFile GetSelectedMap()
        {
            GameConfig config = new GameConfig();
            return config.Maps[GetNumberMap()];
        }
    }
}
