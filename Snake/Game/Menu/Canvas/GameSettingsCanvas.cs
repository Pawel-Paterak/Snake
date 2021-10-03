using Snake.Configurations;
using Snake.Game.Enums;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
using Snake.Game.Settings;
using System;

namespace Snake.Game.Menu.Canvas
{
    class GameSettingsCanvas : ICanvas
    {
        public CanvasEnum Canvas { get; set; }
        public int CountOption { get; set; }
        public Action Render { get; set; }
        public Action Action { get; set; }

        public GameSettingsCanvas()
        {
            Canvas = CanvasEnum.GameSettings;
            CountOption = 4;
            MenuRender menuRender = new MenuRender();
            Render = menuRender.MenuGameRender;
            Action = GameSettingsMenu;
        }

        private void GameSettingsMenu()
        {
            MenuManager menu = MenuManager.Singleton;
            switch ((GameSettingsEnum)menu.ActiveOption)
            {
                case GameSettingsEnum.Play:
                    {
                        GameSettings  gameSettings = new GameSettings();
                        DifficultiGameEnum difficulti = gameSettings.GetSelectedDifficulti();
                        menu.GameManager.Snake.Difficulti = difficulti;
                        int refreshFrame = 28;
                        if (difficulti == DifficultiGameEnum.Easy)
                            refreshFrame = 80;
                        else if (difficulti == DifficultiGameEnum.Medium)
                            refreshFrame = 50;
                        menu.GameManager.RefreshTime = refreshFrame;
                        menu.GameManager.SetMap(gameSettings.GetSelectedMap());
                        menu.IsRenderCanvas = false;
                        break;
                    }
                case GameSettingsEnum.Back:
                    {
                        menu.ActiveCanvas = CanvasEnum.CustomsSnake;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

    }
}
