using System.Collections.Generic;
using Snake.Controlers;
using Snake.Game;
using Snake.Game.Enums;
using Snake.Files.Json;
using Snake.Files;
using Snake.Configurations;

namespace Snake
{
    public class Core
    {
        private readonly KeyboardControl keyboardControl = new KeyboardControl();
        private readonly GameManager game = new GameManager();
        private Menu menu;
        public static bool Closing { get; set; } = false;

        public void Start()
        {
            Veryfications();

            keyboardControl.Start();
            menu = new Menu(game);
            Loop();
            keyboardControl.Close();
        }

        private void Loop()
        {
            do
            {
                if(!Closing)
                    MainMenu();
                if (!Closing)
                    StartGame();
            } while (!Closing);
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
            if(!jManager.Exists(GameConfig.ScoresFile))
                jManager.Write(GameConfig.ScoresFile, new ScoresFile(new List<Score>()));
        }
    }
}
