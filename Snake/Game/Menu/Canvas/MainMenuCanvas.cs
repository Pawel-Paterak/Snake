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
            Action = MenuManager.Singleton.MainMenu;
        }
    }
}
