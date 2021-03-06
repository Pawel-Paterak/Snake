using Snake.Game.Enums;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
using System;

namespace Snake.Game.Menu.Canvas
{
    class ScoresCanvas : ICanvas
    {
        public CanvasEnum Canvas { get; set; }
        public int CountOption { get; set; }
        public Action Render { get; set; }
        public Action Action { get; set; }

        public ScoresCanvas()
        {
            Canvas = CanvasEnum.Scores;
            CountOption = 1;
            MenuRender menuRender = new MenuRender();
            Render = menuRender.MenuScoresRender;
            Action = ScoresMenu;
        }

        private void ScoresMenu()
        {
            MenuManager menu = MenuManager.Singleton;
            switch ((ScoresEnum)menu.ActiveOption)
            {
                case ScoresEnum.back:
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
