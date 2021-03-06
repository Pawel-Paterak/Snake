using Snake.Controlers;
using Snake.Game.Enums;
using Snake.Files;
using Snake.Game.Menu;
using Snake.Game.Managers;

namespace Snake
{
    public class Core
    {
        public static bool Closing { get; set; } = false;

        private readonly KeyboardControl keyboardControl = new KeyboardControl();
        private readonly GameManager game = new GameManager();
        private MenuManager menu;

        public void Start()
        {
            Veryfications();
            keyboardControl.Start();
            menu = new MenuManager(game);
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
            => menu.RenderCanvas(CanvasEnum.MainMenu);

        private void StartGame()
            => game.Start();

        private void Veryfications()
        {
            FileManager file = new FileManager();
            file.ExistsScoresFile();
        }
    }
}
