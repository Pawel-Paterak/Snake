using Snake.Game.Enums;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
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
            Render = menuRender.CustomsSnakeMenuRender;
            Action = MenuManager.singleton.CustomsSnakeMenu;
        }
    }
}
