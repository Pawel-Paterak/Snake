using Snake.Game.Enums;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
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
            Render = menuRender.GameSettingsMenuRender;
            Action = MenuManager.Singleton.GameSettingsMenu;
        }
    }
}
