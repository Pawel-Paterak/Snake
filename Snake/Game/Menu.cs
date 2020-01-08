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
        private int OptionChoose {
            get => _optionChoose;
            set {
                _optionChoose = value;
                menuRender?.Invoke();
            } }
        private int _optionChoose = 0;
        private int OptionChooseColor
        {
            get => _optionChooseColor;
            set
            {
                _optionChooseColor = value;
                menuRender?.Invoke();
            }
        }
        private int _optionChooseColor = 0;
        private int OptionChooseSkin
        {
            get => _optionChooseSkin;
            set
            {
                _optionChooseSkin = value;
                menuRender?.Invoke();
            }
        }
        private int _optionChooseSkin = 0;
        private int countOptionChoose;
        private Action menuRender;
        private Action goInteractionMenu;
        private readonly ConsoleColor[] colorsSnake;
        private readonly char[] skinsSnake;
        private bool IsRenderCanvas { get; set; } = false;

        public Menu(GameManager game)
        {
            this.game = game;

            colorsSnake = new ConsoleColor[8];
            colorsSnake[0] = ConsoleColor.White;
            colorsSnake[1] = ConsoleColor.Cyan;
            colorsSnake[2] = ConsoleColor.Gray;
            colorsSnake[3] = ConsoleColor.Red;
            colorsSnake[4] = ConsoleColor.Yellow;
            colorsSnake[5] = ConsoleColor.Green;
            colorsSnake[6] = ConsoleColor.Blue;
            colorsSnake[7] = ConsoleColor.Magenta;

            skinsSnake = new char[8];
            skinsSnake[0] = '@';
            skinsSnake[1] = '#';
            skinsSnake[2] = '$';
            skinsSnake[3] = '%';
            skinsSnake[4] = 'o';
            skinsSnake[5] = 'm';
            skinsSnake[6] = 'x';
            skinsSnake[7] = 'a';
        }

        public void RenderCanvas()
        {
            KeyboardControl.PressKeyEvent += OnPressKey;

            IsRenderCanvas = true;
            MenuEnum lastMenu = (Canvas != MenuEnum.MainMenu) ? MenuEnum.MainMenu:MenuEnum.Scores;
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
                    case MenuEnum.CustomsSnake:
                        {
                            if (lastMenu != MenuEnum.CustomsSnake)
                            {
                                countOptionChoose = 4;
                                CustomsSnakeMenuRender();
                                lastMenu = Canvas;
                            }
                            menuRender = CustomsSnakeMenuRender;
                            goInteractionMenu = CustomsSnakeMenu;
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
            ConsoleConfig console = new ConsoleConfig();
            Frame();
            string[] options = new string[4];
            options[0] = "Start";
            options[1] = "Multiplayer(disable)";
            options[2] = "Scores";
            options[3] = "Exit";

            int widht = console.Widht / 2;
            int height = console.Height / 2 - options.Length/2;

            for (int i=0; i< options.Length; i++)
            {
                string text = options[i];
                if(i == OptionChoose)
                    text = "> "+text+" <";
                int offsetXText = text.Length / 2;
                render.Write(text, widht - offsetXText, height + 2*i);
            }
        }

        private void Frame()
        {
            ConsoleConfig console = new ConsoleConfig();
            for (int x = 0; x < console.Widht; x++)
            {
                render.Write("#", x, 0);
                render.Write("#", x, console.Height - 2);
            }

            for (int y = 0; y < console.Height - 1; y++)
            {
                render.Write("#", 0, y);
                render.Write("#", console.Widht - 1, y);
            }
        }

        private void MainMenu()
        {
            switch (OptionChoose)
            {
                case 0:
                    {
                        Canvas = MenuEnum.CustomsSnake;
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
            ConsoleConfig console = new ConsoleConfig();
            Frame();
            string[] options = new string[4];
            options[0] = "Easy";
            options[1] = "Medium";
            options[2] = "Hard";
            options[3] = "Back";

            int widht = console.Widht / 2;
            int height = console.Height / 2 - options.Length / 2;

            for (int i = 0; i < options.Length; i++)
            {
                string text = options[i];
                if (i == OptionChoose)
                    text = "> " + text + " <";
                int offsetXText = text.Length / 2;
                render.Write(text, widht - offsetXText, height + 2 * i);
            }
        }

        private void LevelsMenu()
        {
            switch (OptionChoose)
            {
                case 0:
                    {
                        game.Snake.Difficulti = DifficultiGame.Easy;
                        game.RefreshTime = 100;
                        IsRenderCanvas = false;
                        break;
                    }
                case 1:
                    {
                        game.Snake.Difficulti = DifficultiGame.Medium;
                        game.RefreshTime = 60;
                        IsRenderCanvas = false;
                        break;
                    }
                case 2:
                    {
                        game.Snake.Difficulti = DifficultiGame.Hard;
                        game.RefreshTime = 30;
                        IsRenderCanvas = false;
                        break;
                    }
                case 3:
                    {
                        Canvas = MenuEnum.CustomsSnake;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void CustomsSnakeMenuRender()
        {
            render.Clear();
            ConsoleConfig console = new ConsoleConfig();
            Frame();
            string[] options = new string[4];
            options[0] = "Color";
            options[1] = "Skin";
            options[2] = "Play";
            options[3] = "Back";
            int widht = console.Widht / 2;
            int height = console.Height / 2 - (options.Length+2) / 2;

            for (int i = 0; i < options.Length; i++)
            {
                string text = options[i];
                if (OptionChoose == i && i != 0 && i != 1)
                    text = "> " + text + " <";

                if (OptionChoose != 0 && i == 0)
                    text += " " + colorsSnake[OptionChooseColor];

                if (OptionChoose != 1 && i == 1)
                    text += " " + skinsSnake[OptionChooseSkin];

                int offsetXText = text.Length / 2;
                render.Write(text, widht - offsetXText, height + 2*i);

                if (OptionChoose == 0 && i == 0)
                {
                    int index = OptionChooseColor - 1;
                    if (index < 0)
                        index = colorsSnake.Length-1;
                    text = colorsSnake[index] + " ";

                    index = OptionChooseColor;
                    if (index < 0 || index > colorsSnake.Length - 1)
                        index = 0;
                    text += "> "+colorsSnake[index] + " < ";

                    index = OptionChooseColor + 1;
                    if (index > colorsSnake.Length - 1)
                        index = 0;
                    text += colorsSnake[index];

                    offsetXText = text.Length / 2;
                    render.Write(text, widht - offsetXText, height + 2 * i);
                }

                if (OptionChoose == 1 && i == 1)
                {
                    int index = OptionChooseSkin - 1;
                    if (index < 0)
                        index = skinsSnake.Length - 1;
                    text = skinsSnake[index] + " ";

                    index = OptionChooseSkin;
                    if (index < 0 || index > skinsSnake.Length - 1)
                        index = 0;
                    text += "> "+skinsSnake[index] + " < ";

                    index = OptionChooseSkin + 1;
                    if (index > skinsSnake.Length - 1)
                        index = 0;
                    text += skinsSnake[index];

                    offsetXText = text.Length / 2;
                    render.Write(text, widht - offsetXText, height + 2 * i);
                }
            }
        }

        private void CustomsSnakeMenu()
        {
            switch (OptionChoose)
            {
                case 2:
                    {
                        game.Snake.ColorSnake = colorsSnake[OptionChooseColor];
                        game.Snake.SkinSnake = skinsSnake[OptionChooseSkin];
                        Canvas = MenuEnum.Levels;
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
            Frame();
            JsonManager json = new JsonManager();
            ScoresFile scoresFile = json.Read<ScoresFile>(GameConfig.ScoresFile);
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
            switch (OptionChoose)
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
            if (key == ConsoleKey.Enter)
            {
                goInteractionMenu?.Invoke();
                _optionChoose = 0;
            }

            if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                OptionChoose--;
            if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                OptionChoose++;

            if (OptionChoose < 0)
                OptionChoose = countOptionChoose-1;
            else if (OptionChoose >= countOptionChoose)
                OptionChoose = 0;

            if(Canvas == MenuEnum.CustomsSnake)
            {
                if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                {
                    if (OptionChoose == 0)
                        OptionChooseColor--;
                    else if (OptionChoose == 1)
                        OptionChooseSkin--;
                }
                if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                {
                    if (OptionChoose == 0)
                        OptionChooseColor++;
                    else if (OptionChoose == 1)
                        OptionChooseSkin++;
                }
            }

            if (OptionChooseColor < 0)
                OptionChooseColor = colorsSnake.Length - 1;
            else if (OptionChooseColor >= colorsSnake.Length)
                OptionChooseColor = 0;

            if (OptionChooseSkin < 0)
                OptionChooseSkin = skinsSnake.Length - 1;
            else if (OptionChooseSkin >= skinsSnake.Length)
                OptionChooseSkin = 0;
        }

        private void CloseConsole()
        {
            Core.Closing = true;
            IsRenderCanvas = false;
            //Environment.Exit(0);
        }

        public void GameoverMenu(int scores)
        {
            string text = "GameOver, you scores " + scores;
            ConsoleConfig console = new ConsoleConfig();
            game.WaitForPlayerName = true;
            string nameRender = "";
            bool firstTime = true;
            do
            {
                if (game.Name != nameRender || firstTime)
                {
                    firstTime = false;
                    nameRender = game.Name;
                    render.Clear();
                    render.Write(text, console.Widht / 2 - text.Length / 2, console.Height / 2);
                    render.Write(nameRender, console.Widht / 2 - nameRender.Length / 2, console.Height / 2 + 2);
                }
            } while (game.WaitForPlayerName);
        }
    }
}
