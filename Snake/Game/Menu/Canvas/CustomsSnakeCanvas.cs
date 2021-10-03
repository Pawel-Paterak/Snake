using Snake.Configurations;
using Snake.Game.Enums;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
using Snake.Game.Settings;
using System;

namespace Snake.Game.Menu.Canvas
{
    class CustomsSnakeCanvas : ICanvas
    {
        public CanvasEnum Canvas { get; set; }
        public int CountOption { get; set; }
        public Action Render { get; set; }
        public Action Action { get; set; }

        public CustomsSnakeCanvas()
        {
            Canvas = CanvasEnum.CustomsSnake;
            CountOption = 4;
            MenuRender menuRender = new MenuRender();
            Render = menuRender.MenuSnakeRender;
            Action = MenuSnakeAction;
        }

        private void MenuSnakeAction()
        {
            MenuManager menu = MenuManager.Singleton;
            switch ((CustomsSnakeEnum)menu.ActiveOption)
            {
                case CustomsSnakeEnum.play:
                    {
                        GameSettings settings = new GameSettings();
                        GameConfig config = new GameConfig();
                        menu.GameManager.Snake.ColorSnake = config.Colors[settings.GetNumberColor()];
                        menu.GameManager.Snake.SkinSnake = config.Skins[settings.GetNumberSkin()];
                        menu.ActiveCanvas = CanvasEnum.GameSettings;
                        break;
                    }
                case CustomsSnakeEnum.back:
                    {
                        menu.ActiveCanvas = CanvasEnum.MainMenu;
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
