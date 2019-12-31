using Snake.Controlers;
using Snake.Game;
using Snake.Game.Enums;
using System;

namespace Snake
{
    public class Core
    {
        private GameManager game = new GameManager();
        private Menu menu;

        public void Start()
        {
            menu = new Menu(game);
            menu.Canvas = MenuEnum.MainMenu;
            menu.RenderCanvas();

            game.Start();
            KeyboardControl.Close();
            Console.ReadKey();
        }
    }
}
