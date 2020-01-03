using Snake.Configurations;
using Snake.Controlers;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Enums;
using Snake.Game.Render;
using System;

namespace Snake.Game
{
    public class Menu
    {
        public MenuEnum Canvas { get; set; }

        private readonly GameManager game;
        private readonly ConsoleRender render = new ConsoleRender();
        private readonly Func<ConsoleKey> getKey;
        private bool isNewKey = false;
        private ConsoleKey Key { get => key;
            set
            {
                key = value;
                isNewKey = true;
            }
        }
        private ConsoleKey key;

        public Menu(GameManager game)
        {
            this.game = game;
            KeyboardControl.Start();
            getKey = GetKey;
        }

        public void RenderCanvas()
        {
            KeyboardControl.PressKeyEvent += OnPressKey;

            bool isLoop = true;
            do
            {
                switch (Canvas)
                {
                    case MenuEnum.MainMenu:
                        {
                            MainMenuRender();
                            isLoop = !MainMenu(getKey());
                            break;
                        }
                    case MenuEnum.Levels:
                        {
                            LevelsMenuRender();
                            isLoop = !LevelsMenu(getKey());
                            break;
                        }
                    case MenuEnum.Scores:
                        {
                            ScoresMenuRender();
                            isLoop = !ScoresMenu(getKey());
                            break;
                        }
                }
            } while (isLoop);
            KeyboardControl.PressKeyEvent -= OnPressKey;
        }

        private void MainMenuRender()
        {
            render.Clear();
            ConsoleConfiguration console = new ConsoleConfiguration();
            Frame();
            int offsetXText = 5;
            int widht = console.widht / 2;
            int height = console.height / 2;
            string text = "1) Start";
            render.Write(text, widht - offsetXText, height - 3);
            text = "2) Multiplayer(disable)";
            render.Write(text, widht- offsetXText, height-1);
            text = "3) Scores";
            render.Write(text, widht - offsetXText, height+1);
            text = "4) Exit";
            render.Write(text, widht - offsetXText, height+3);
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

        private bool MainMenu(ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.D1:
                    {
                        Canvas = MenuEnum.Levels;
                        return false;
                    }
                case ConsoleKey.D3:
                    {
                        Canvas = MenuEnum.Scores;
                        return false;
                    }
                case ConsoleKey.D4:
                    {
                        CloseConsole();
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        private void LevelsMenuRender()
        {
            render.Clear();
            ConsoleConfiguration console = new ConsoleConfiguration();
            Frame();
            int offsetXText = 5;
            int widht = console.widht / 2;
            int height = console.height / 2;
            string text = "1) Easy";
            render.Write(text, widht - offsetXText, height - 3);
            text = "2) Medium";
            render.Write(text, widht - offsetXText, height - 1);
            text = "3) Hard";
            render.Write(text, widht - offsetXText, height + 1);
            text = "4) Back";
            render.Write(text, widht - offsetXText, height + 3);
        }

        private bool LevelsMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                    {
                        game.RefreshTime = 100;
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        game.RefreshTime = 60;
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        game.RefreshTime = 30;
                        break;
                    }
                case ConsoleKey.D4:
                    {
                        Canvas = MenuEnum.MainMenu;
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
            return true;
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

            render.Write("1) back", 2, 36);
        }

        private bool ScoresMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                    {
                        Canvas = MenuEnum.MainMenu;
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
            return true;
        }

        private void OnPressKey(ConsoleKey key)
            => Key = key;

        private ConsoleKey GetKey()
        {
            bool isLoop = true;
            ConsoleKey myKey = ConsoleKey.A;
            do
            {
                if (isNewKey)
                {
                    isLoop = false;
                    isNewKey = false;
                    myKey = Key;
                }

            } while (isLoop);
            return myKey;
        }

        private void CloseConsole()
        {
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
