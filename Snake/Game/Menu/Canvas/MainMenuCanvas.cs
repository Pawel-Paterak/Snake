using Snake.Game.Enums;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
using System;

namespace Snake.Game.Menu.Canvas
{
    public class MainMenuCanvas : ICanvas
    {
        public CanvasEnum Canvas { get; set; }
        public int CountOption { get; set; }
        public Action Render { get; set; }
        public Action Action { get; set; }

        public MainMenuCanvas()
        {
            Canvas = CanvasEnum.MainMenu;
            CountOption = 4;
            MenuRender menuRender = new MenuRender();
            Render = menuRender.MainMenuRender;
            Action = MainMenu;
        }

        private void MainMenu()
        {
            MenuManager menu = MenuManager.Singleton;
            switch ((MainMenuEnums)menu.ActiveOption)
            {
                case MainMenuEnums.CustomsSnake:
                    {
                        menu.ActiveCanvas = CanvasEnum.CustomsSnake;
                        break;
                    }
                case MainMenuEnums.Scores:
                    {
                        menu.ActiveCanvas = CanvasEnum.Scores;
                        break;
                    }
                case MainMenuEnums.Exit:
                    {
                        CloseConsole();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void CloseConsole()
        {
            Core.Closing = true;
            MenuManager.Singleton.IsRenderCanvas = false;
        }
    }
}
