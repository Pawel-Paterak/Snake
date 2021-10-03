using Snake.Configurations;
using Snake.Controlers;
using Snake.Game.Enums;
using Snake.Game.Managers;
using Snake.Game.Menu.Canvas;
using Snake.Game.Menu.Interface;
using Snake.Game.Render;
using Snake.Game.Settings;
using System;
using System.Collections.Generic;

namespace Snake.Game.Menu
{
    public class MenuManager
    {
        public int ActiveOption
        {
            get => activeOption;
            set
            {
                activeOption = value;
                if (activeOption < 0)
                    activeOption = activeCanvas.CountOption - 1;
                else if (activeOption >= activeCanvas.CountOption)
                    activeOption = 0;
                activeCanvas.Render?.Invoke();
            }
        }
        public bool IsRenderCanvas { get; set; }
        public GameManager GameManager { get; private set; }
        public Dictionary<CanvasEnum, IntigerBox[]> AdvencedOptions { get; private set; }
        public CanvasEnum ActiveCanvas
        {
            get => activeCanvas.Canvas;
            set => SetCanvas(value);
        }
        public static MenuManager Singleton { get; set; }

        private ICanvas activeCanvas;
        private int activeOption = 0;
        private readonly Dictionary<CanvasEnum, ICanvas> canvas = new Dictionary<CanvasEnum, ICanvas>();

        private readonly ConsoleRender render = new ConsoleRender();

        public MenuManager()
        {

        }

        public MenuManager(GameManager game)
        {
            if (Singleton == null)
                Singleton = this;

            InitializeCanvas();
            InitializeAdvencedOptions();

            GameManager = game;
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

        public void GameOverMenu(int scores)
        {
            string text = "GameOver, you scores " + scores;
            ConsoleConfig console = new ConsoleConfig();
            GameManager.WaitForPlayerName = true;
            string nameRender = "";
            bool firstTime = true;
            do
            {
                if (GameSettings.PlayerName != nameRender || firstTime)
                {
                    firstTime = false;
                    nameRender = GameSettings.PlayerName;
                    render.Clear();
                    render.Write(text, console.Widht / 2 - text.Length / 2, console.Height / 2);
                    render.Write(nameRender, console.Widht / 2 - nameRender.Length / 2, console.Height / 2 + 2);
                }
            } while (GameManager.WaitForPlayerName);
        }

        public int GetActiveOption()
            => Singleton.ActiveOption;

        private void SetCanvas(CanvasEnum canvas)
        {
            if (this.canvas.ContainsKey(canvas))
            {
                activeCanvas = this.canvas[canvas];
                activeCanvas.Render?.Invoke();
            }
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

        private void InitializeAdvencedOptions()
        {
            GameConfig config = new GameConfig();
            MenuConfig menuConfig = new MenuConfig();
            AdvencedOptions = new Dictionary<CanvasEnum, IntigerBox[]>();
            IntigerBox[] customsBox = new IntigerBox[menuConfig.CountMenuCustomsSnakeOptions];
            IntigerBox[] gameSettingsBox = new IntigerBox[menuConfig.CountMenuGameOptions];
            AdvencedOptions.Add(CanvasEnum.CustomsSnake, customsBox);
            AdvencedOptions.Add(CanvasEnum.GameSettings, gameSettingsBox);
            AdvencedOptions[CanvasEnum.CustomsSnake][0] = new IntigerBox(0, config.Colors.Length);
            AdvencedOptions[CanvasEnum.CustomsSnake][1] = new IntigerBox(0, config.Skins.Length);
            AdvencedOptions[CanvasEnum.GameSettings][0] = new IntigerBox(0, config.Maps.Length);
            AdvencedOptions[CanvasEnum.GameSettings][1] = new IntigerBox(0, config.Difficulti.Length);
        }

        private void OnPressKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                activeCanvas.Action?.Invoke();
                ActiveOption = 0;
            }

            if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                ActiveOption--;
            if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                ActiveOption++;

            if (AdvencedOptions.ContainsKey(ActiveCanvas) && AdvencedOptions[ActiveCanvas][activeOption] != null)
            {
                if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                {
                    AdvencedOptions[ActiveCanvas][activeOption].Intiger--;
                    activeCanvas.Render?.Invoke();
                }

                if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                {
                    AdvencedOptions[ActiveCanvas][activeOption].Intiger++;
                    activeCanvas.Render?.Invoke();
                }
            }
        }
    }
}