using Snake.Game.Enums;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
using System;

namespace Snake.Game.Menu.Canvas
{
    class LevelsCanvas : ICanvas
    {
        public CanvasEnum Canvas { get; set; }
        public int CountOption { get; set; }
        public Action Render { get; set; }
        public Action Action { get; set; }

        public LevelsCanvas()
        {
            Canvas = CanvasEnum.Levels;
            CountOption = 4;
            MenuRender menuRender = new MenuRender();
            Render = menuRender.LevelsMenuRender;
            Action = MenuManager.Singleton.LevelsMenu;
        }
    }
}
