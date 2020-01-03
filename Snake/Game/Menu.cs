using Snake.Configurations;
using Snake.Controlers;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Enums;
using Snake.Game.Render;
using System;
using System.Threading;

namespace Snake.Game
{
    public class Menu
    {
        public MenuEnum Canvas { get; set; }

        private readonly GameManager game;
        private readonly ConsoleRender render = new ConsoleRender();
        private int optionChoose {
            get => _optionChoose;
            set {
                _optionChoose = value;
                menuRender?.Invoke();
            } }
        private int _optionChoose = 0;
        private int countOptionChoose;
        private Action menuRender;
        private Action goInteractionMenu;
        private bool IsRenderCanvas { get; set; } = false;

        public Menu(GameManager game)
        {
            this.game = game;
        }

        public void RenderCanvas()
        {
            KeyboardControl.PressKeyEvent += OnPressKey;

            IsRenderCanvas = true;
            MenuEnum lastMenu = MenuEnum.Scores;
            do
            {
                switch (Canvas)
                {
                    case MenuEnum.MainMenu:
                        {
                            if (lastMenu != MenuEnum.MainMenu)
                            {
                                countOptionChoose = 4;
                                MainMenuRender();
                                lastMenu = Canvas;
                            }
                            menuRender = MainMenuRender;
                            goInteractionMenu = MainMenu;
                            break;
                        }
                    case MenuEnum.Levels:
                        {
                            if (lastMenu != MenuEnum.Levels)
                            {
                                countOptionChoose = 4;
                                LevelsMenuRender();
                                lastMenu = Canvas;
                            }
                            menuRender = LevelsMenuRender;
                            goInteractionMenu = LevelsMenu;
                            break;
                        }
                    case MenuEnum.Scores:
                        {
                            if (lastMenu != MenuEnum.Scores)
                            {
                                countOptionChoose = 1;
                                ScoresMenuRender();
                                lastMenu = Canvas;
                            }
                            menuRender = ScoresMenuRender;
                            goInteractionMenu = ScoresMenu;
                            break;
                        }
                }
            } while (IsRenderCanvas);
            KeyboardControl.PressKeyEvent -= OnPressKey;
        }

        private void MainMenuRender()
        {
            render.Clear();
            ConsoleConfiguration console = new ConsoleConfiguration();
            Frame();
            string[] options = new string[4];
            options[0] = "Start";
            options[1] = "Multiplayer(disable)";
            options[2] = "Scores";
            options[3] = "Exit";

            int widht = console.widht / 2;
            int height = console.height / 2 - options.Length/2;

            for (int i=0; i< options.Length; i++)
            {
                string text = options[i];
                if(i == optionChoose)
                    text = "> "+text+" <";
                int offsetXText = text.Length / 2;
                render.Write(text, widht - offsetXText, height + 2*i);
            }
        }

        private void Frame()
        {
            ConsoleConfiguration console = new ConsoleConfiguration();
            for (int x = 0; x < console.widht; x++)
            {
                render.Write("#", x, 0);
                render.Write("#", x, console.height - 2);
            }

            for (int y = 0; y < console.height - 1; y++)
            {
                render.Write("#", 0, y);
                render.Write("#", console.widht - 1, y);
            }
        }

        private void MainMenu()
        {
            switch (optionChoose)
            {
                case 0:
                    {
                        Canvas = MenuEnum.Levels;
                        break;
                    }
                case 2:
                    {
                        Canvas = MenuEnum.Scores;
                        break;
                    }
                case 3:
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

        private void LevelsMenuRender()
        {
            render.Clear();
            ConsoleConfiguration console = new ConsoleConfiguration();
            Frame();
            string[] options = new string[4];
            options[0] = "Easy";
            options[1] = "Medium";
            options[2] = "Hard";
            options[3] = "Back";

            int widht = console.widht / 2;
            int height = console.height / 2 - options.Length / 2;

            for (int i = 0; i < options.Length; i++)
            {
                string text = options[i];
                if (i == optionChoose)
                    text = "> " + text + " <";
                int offsetXText = text.Length / 2;
                render.Write(text, widht - offsetXText, height + 2 * i);
            }
        }

        private void LevelsMenu()
        {
            switch (optionChoose)
            {
                case 0:
                    {
                        game.RefreshTime = 100;
                        IsRenderCanvas = false;
                        break;
                    }
                case 1:
                    {
                        game.RefreshTime = 60;
                        IsRenderCanvas = false;
                        break;
                    }
                case 2:
                    {
                        game.RefreshTime = 30;
                        IsRenderCanvas = false;
                        break;
                    }
                case 3:
                    {
                        Canvas = MenuEnum.MainMenu;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void ScoresMenuRender()
        {
            render.Clear();
            ConsoleConfiguration console = new ConsoleConfiguration();
            Frame();
            JsonManager json = new JsonManager();
            ScoresFile scoresFile = json.Read<ScoresFile>("scores.json");
            for (int i=0; i<17; i++)
            {
                string text = (i + 1) + ": ";
                if (scoresFile != null && i < scoresFile.Scores.Count)
                    text += scoresFile.Scores[i].Name + " " + scoresFile.Scores[i].Scores;
                render.Write(text, 2, 2+2*i);
            }

            render.Write("> back <", 2, 36);
        }

        private void ScoresMenu()
        {
            switch (optionChoose)
            {
                case 0:
                    {
                        Canvas = MenuEnum.MainMenu;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void OnPressKey(ConsoleKey key)
        {
            if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                optionChoose--;
            if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                optionChoose++;

            if (optionChoose < 0)
                optionChoose = countOptionChoose-1;
            else if (optionChoose >= countOptionChoose)
                optionChoose = 0;

            if (key == ConsoleKey.Enter)
                goInteractionMenu?.Invoke();
        }

        private void CloseConsole()
        {
            IsRenderCanvas = false;
            KeyboardControl.Close();
            Environment.Exit(0);
        }

        public void GameoverMenu(int scores)
        {
            string text = "GameOver, you scores " + scores;
            ConsoleConfiguration console = new ConsoleConfiguration();
            game.waitForPlayerName = true;
            string nameRender = "";
            do
            {
                if (game.name != nameRender)
                {
                    nameRender = game.name;
                    render.Clear();
                    render.Write(text, console.widht / 2 - text.Length / 2, console.height / 2);
                    render.Write(nameRender, console.widht / 2 - nameRender.Length / 2, console.height / 2 + 2);
                }
            } while (game.waitForPlayerName);
        }
    }
}
