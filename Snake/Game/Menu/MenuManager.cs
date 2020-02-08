using Snake.Configurations;
using Snake.Controlers;
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

        private CanvasEnum ActiveCanvas
        {
            get => activeCanvas.Canvas;
            set => SetCanvas(value);

        }
        private ICanvas activeCanvas;
        private int optionChoose = 0;
        private bool isRenderCanvas;
        private OptionChoose[] optionsChosseX;
        private Dictionary<CanvasEnum, ICanvas> canvas = new Dictionary<CanvasEnum, ICanvas>();
        private GameManager game;
        private ConsoleRender render = new ConsoleRender();

        public static MenuManager Singleton { get; set; }

        public MenuManager()
        {

        }

        public MenuManager(GameManager game)
        {
            if (Singleton == null)
                Singleton = this;

            InitializeCanvas();
            GameConfig config = new GameConfig();
            optionsChosseX = new OptionChoose[4];
            optionsChosseX[0] = new OptionChoose(0, config.CountColors);
            optionsChosseX[1] = new OptionChoose(0, config.CountSkins);
            optionsChosseX[2] = new OptionChoose(0, config.CountMap);
            optionsChosseX[3] = new OptionChoose(0, config.CountDifficulti);

            this.game = game;
        }

        public void RenderCanvas(CanvasEnum canvas)
        {
            KeyboardControl.PressKeyEvent += OnPressKey;

            ActiveCanvas = canvas;
            isRenderCanvas = true;
            do
            { }
            while (isRenderCanvas);

            KeyboardControl.PressKeyEvent -= OnPressKey;
        }

        public void MainMenu()
        {
            switch ((MainMenuEnum)Singleton.OptionChoose)
            {
                case MainMenuEnum.CustomsSnake:
                    {
                        ActiveCanvas = CanvasEnum.CustomsSnake;
                        break;
                    }
                case MainMenuEnum.Scores:
                    {
                        ActiveCanvas = CanvasEnum.Scores;
                        break;
                    }
                case MainMenuEnum.Exit:
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

        public void CustomsSnakeMenu()
        {
            switch ((CustomsSnakeEnum)Singleton.OptionChoose)
            {
                case CustomsSnakeEnum.play:
                    {
                        GameConfig config = new GameConfig();
                        game.Snake.ColorSnake = config.ColorsSnake[GetChooseColor()];
                        game.Snake.SkinSnake = config.SkinsSnake[GetChooseSkins()];
                        ActiveCanvas = CanvasEnum.GameSettings;
                        break;
                    }
                case CustomsSnakeEnum.back:
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

        public void GameSettingsMenu()
        {
            switch ((GameSettingsEnum)Singleton.OptionChoose)
            {
                case GameSettingsEnum.Play:
                    {
                        GameConfig gameConfig = new GameConfig();
                        DifficultiGameEnum difficulti = gameConfig.Difficulti[GetChooseDifficulti()];
                        game.Snake.Difficulti = difficulti;
                        int refreshFrame = 28;
                        if (difficulti == DifficultiGameEnum.Easy)
                            refreshFrame = 80;
                        else if (difficulti == DifficultiGameEnum.Medium)
                            refreshFrame = 50;
                        game.RefreshTime = refreshFrame;
                        game.Map = gameConfig.Maps[GetChooseMap()];
                        isRenderCanvas = false;
                        break;
                    }
                case GameSettingsEnum.Back:
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

        public void ScoresMenu()
        {
            switch ((ScoresEnum)Singleton.OptionChoose)
            {
                case ScoresEnum.back:
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

        public void GameOverMenu(int scores)
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

        public int GetOptionChoose()
            => Singleton.OptionChoose;

        public int GetChooseColor()
         => Singleton.optionsChosseX[0].Option;

        public int GetChooseSkins()
         => Singleton.optionsChosseX[1].Option;

        public int GetChooseMap()
         => Singleton.optionsChosseX[2].Option;

        public int GetChooseDifficulti()
         => Singleton.optionsChosseX[3].Option;

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
            isRenderCanvas = false;
        }

        private void InitializeCanvas()
        {
            MainMenuCanvas mainMenu = new MainMenuCanvas();
            canvas.Add(mainMenu.Canvas, mainMenu);
            ScoresCanvas scores = new ScoresCanvas();
            canvas.Add(scores.Canvas, scores);
            CustomsSnakeCanvas customsSnake = new CustomsSnakeCanvas();
            canvas.Add(customsSnake.Canvas, customsSnake);
            GameSettingsCanvas levels = new GameSettingsCanvas();
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

            if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
            {
                if (optionChoose == 0 || optionChoose == 1)
                {
                    if (ActiveCanvas == CanvasEnum.GameSettings)
                        optionsChosseX[optionChoose + 2].Option--;
                    else
                        optionsChosseX[optionChoose].Option--;
                    activeCanvas.Render?.Invoke();
                }
            }
            if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
            {
                if (optionChoose == 0 || optionChoose == 1)
                {
                    if (ActiveCanvas == CanvasEnum.GameSettings)
                        optionsChosseX[optionChoose + 2].Option++;
                    else
                        optionsChosseX[optionChoose].Option++;
                    activeCanvas.Render?.Invoke();
                }
            }

        }
    }
}
