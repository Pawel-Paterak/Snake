using System.Collections.Generic;
using Snake.Controlers;
using Snake.Game;
using Snake.Game.Enums;
using Snake.Files.Json;
using Snake.Files;

namespace Snake
{
    public class Core
    {
        private GameManager game = new GameManager();
        private Menu menu;

        public void Start()
        {
            Veryfications();

            menu = new Menu(game);
            Loop();
            KeyboardControl.Close();
        }

        private void Loop()
        {
            bool isLoop = true;
            do
            {
                MainMenu();
                StartGame();
            } while (isLoop);
        }

        private void MainMenu()
        {
            menu.Canvas = MenuEnum.MainMenu;
            menu.RenderCanvas();
        }

        private void StartGame()
        {
            game.Start();
        }

        private void Veryfications()
        {
            JsonManager jManager = new JsonManager();
            if(!jManager.Exists("scores.json"))
                jManager.Write("scores.json", new ScoresFile(new List<Score>()));
        }
    }
}
