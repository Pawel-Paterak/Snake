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
        private bool IsRenderCanvas { get; set; } = false;
        private OptionChoose[] OptionsChosseX { get; set; }
        private Dictionary<CanvasEnum, ICanvas> Canvas { get; set; } = new Dictionary<CanvasEnum, ICanvas>();
        private GameManager Game { get; set; }
        private ConsoleRender Render { get; set; } = new ConsoleRender();

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
            OptionsChosseX = new OptionChoose[4];
            OptionsChosseX[0] = new OptionChoose(0, config.CountColors);
            OptionsChosseX[1] = new OptionChoose(0, config.CountSkins);
            OptionsChosseX[2] = new OptionChoose(0, config.CountMap);
            OptionsChosseX[3] = new OptionChoose(0, config.CountDifficulti);

            Game = game;
        }
        public void RenderCanvas(CanvasEnum canvas)
        {
            KeyboardControl.PressKeyEvent += OnPressKey;

            ActiveCanvas = canvas;
            IsRenderCanvas = true;
            do
            { }
            while (IsRenderCanvas);

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
                        Game.Snake.ColorSnake = config.ColorsSnake[GetChooseColor()];
                        Game.Snake.SkinSnake = config.SkinsSnake[GetChooseSkins()];
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
                        GameConfig config = new GameConfig();
                        DifficultiGameEnum difficulti = config.Difficulti[GetChooseDifficulti()];
                        Game.Snake.Difficulti = difficulti;
                        int refreshFrame = 28;
                        if (difficulti == DifficultiGameEnum.Easy)
                            refreshFrame = 80;
                        else if (difficulti == DifficultiGameEnum.Medium)
                            refreshFrame = 50;
                        Game.RefreshTime = refreshFrame;
                        Game.Map = config.Maps[GetChooseMap()];
                        IsRenderCanvas = false;
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
            Game.WaitForPlayerName = true;
            string nameRender = "";
            bool firstTime = true;
            do
            {
                if (Game.Name != nameRender || firstTime)
                {
                    firstTime = false;
                    nameRender = Game.Name;
                    Render.Clear();
                    Render.Write(text, console.Widht / 2 - text.Length / 2, console.Height / 2);
                    Render.Write(nameRender, console.Widht / 2 - nameRender.Length / 2, console.Height / 2 + 2);
                }
            } while (Game.WaitForPlayerName);
        }
        public int GetOptionChoose()
            => Singleton.OptionChoose;
        public int GetChooseColor()
         => Singleton.OptionsChosseX[0].Option;
        public int GetChooseSkins()
         => Singleton.OptionsChosseX[1].Option;
        public int GetChooseMap()
         => Singleton.OptionsChosseX[2].Option;
        public int GetChooseDifficulti()
         => Singleton.OptionsChosseX[3].Option;

        private void SetCanvas(CanvasEnum canvas)
        {
            if (this.Canvas.ContainsKey(canvas))
            {
                activeCanvas = this.Canvas[canvas];
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
            Canvas.Add(mainMenu.Canvas, mainMenu);
            ScoresCanvas scores = new ScoresCanvas();
            Canvas.Add(scores.Canvas, scores);
            CustomsSnakeCanvas customsSnake = new CustomsSnakeCanvas();
            Canvas.Add(customsSnake.Canvas, customsSnake);
            GameSettingsCanvas levels = new GameSettingsCanvas();
            Canvas.Add(levels.Canvas, levels);
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
                        OptionsChosseX[optionChoose + 2].Option--;
                    else
                        OptionsChosseX[optionChoose].Option--;
                    activeCanvas.Render?.Invoke();
                }
            }
            if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
            {
                if (optionChoose == 0 || optionChoose == 1)
                {
                    if (ActiveCanvas == CanvasEnum.GameSettings)
                        OptionsChosseX[optionChoose + 2].Option++;
                    else
                        OptionsChosseX[optionChoose].Option++;
                    activeCanvas.Render?.Invoke();
                }
            }

        }
    }
}
