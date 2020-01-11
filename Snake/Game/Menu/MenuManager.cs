using Snake.Configurations;
using Snake.Controlers;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Enums;
using Snake.Game.Menu.Canvas;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
using System;
using System.Collections.Generic;

namespace Snake.Game.Menu
{
    public class MenuManager
    {
        public int OptionChoose
        {
            get => optionChoose;
            set
            {
                optionChoose = value;
                if (optionChoose < 0)
                    optionChoose = activeCanvas.CountOption - 1;
                else if (optionChoose >= activeCanvas.CountOption)
                    optionChoose = 0;
                activeCanvas.Render?.Invoke();
            }
        }
        public static MenuManager singleton { get; private set; }

        private CanvasEnum ActiveCanvas
        {
            get => activeCanvas.Canvas;
            set => SetCanvas(value);

        }
        private ICanvas activeCanvas;
        private int optionChoose = 0;
        private bool IsRenderCanvas { get; set; } = false;
        private OptionChoose[] optionsChosseX { get; set; }
        private Dictionary<CanvasEnum, ICanvas> canvas { get; set; } = new Dictionary<CanvasEnum, ICanvas>();
        private GameManager game { get; set; }
        private ConsoleRender render { get; set; } = new ConsoleRender();

        public MenuManager(GameManager game)
        {
            if (singleton == null)
                singleton = this;

            InitializeCanvas();

            optionsChosseX = new OptionChoose[2];
            optionsChosseX[0] = new OptionChoose(0, GameConfig.CountColors);
            optionsChosseX[1] = new OptionChoose(0, GameConfig.CountSkins);

            this.game = game;
        }
        public void RenderCanvas(CanvasEnum canvas)
        {
            KeyboardControl.PressKeyEvent += OnPressKey;

            ActiveCanvas = canvas;
            IsRenderCanvas = true;
            do
            {}
            while (IsRenderCanvas);

            KeyboardControl.PressKeyEvent -= OnPressKey;
        }
        public void MainMenu()
        {
            switch (OptionChoose)
            {
                case 0:
                    {
                        ActiveCanvas = CanvasEnum.CustomsSnake;
                        break;
                    }
                case 2:
                    {
                        ActiveCanvas = CanvasEnum.Scores;
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
        public void LevelsMenu()
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
                        ActiveCanvas = CanvasEnum.CustomsSnake;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public void CustomsSnakeMenu()
        {
            switch (OptionChoose)
            {
                case 2:
                    {
                        GameConfig config = new GameConfig();
                        game.Snake.ColorSnake = config.ColorsSnake[GetChooseColor()];
                        game.Snake.SkinSnake = config.SkinsSnake[GetChooseSkins()];
                        ActiveCanvas = CanvasEnum.Levels;
                        break;
                    }
                case 3:
                    {
                        ActiveCanvas = CanvasEnum.MainMenu;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public void ScoresMenu()
        {
            switch (OptionChoose)
            {
                case 0:
                    {
                        ActiveCanvas = CanvasEnum.MainMenu;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
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
        public int GetChooseColor()
         => optionsChosseX[0].Option;
        public int GetChooseSkins()
         => optionsChosseX[1].Option;

        private void SetCanvas(CanvasEnum canvas)
        {
            if (this.canvas.ContainsKey(canvas))
            {
                activeCanvas = this.canvas[canvas];
                activeCanvas.Render?.Invoke();
            }
        }
        private void CloseConsole()
        {
            Core.Closing = true;
            IsRenderCanvas = false;
        }
        private void InitializeCanvas()
        {
            MainMenuCanvas mainMenu = new MainMenuCanvas();
            canvas.Add(mainMenu.Canvas, mainMenu);
            ScoresCanvas scores = new ScoresCanvas();
            canvas.Add(scores.Canvas, scores);
            CustomsSnakeCanvas customsSnake = new CustomsSnakeCanvas();
            canvas.Add(customsSnake.Canvas, customsSnake);
            LevelsCanvas levels = new LevelsCanvas();
            canvas.Add(levels.Canvas, levels);
        }
        private void OnPressKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                activeCanvas.Action?.Invoke();
                OptionChoose = 0;
            }

            if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                OptionChoose--;
            if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                OptionChoose++;

            if(ActiveCanvas == CanvasEnum.CustomsSnake)
            {
                if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                {
                    if (optionChoose == 0 || optionChoose == 1)
                    {
                        optionsChosseX[optionChoose].Option--;
                        activeCanvas.Render?.Invoke();
                    }
                }
                if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                {
                    if (optionChoose == 0 || optionChoose == 1)
                    {
                        optionsChosseX[optionChoose].Option++;
                        activeCanvas.Render?.Invoke();
                    }
                }
            }
        }
    }
}
