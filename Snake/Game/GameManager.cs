using Snake.Configurations;
using Snake.Controlers;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Menu;
using Snake.Game.Render;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake.Game
{
    public class GameManager
    {
        public int RefreshTime { get; set; } = 50;
        public string Name { get; private set; } = "";
        public bool WaitForPlayerName { get; set; } = false;
        public Snake Snake { get; private set; } = new Snake();
        public MapFile Map { get; set; }

        private readonly ConsoleRender render = new ConsoleRender();
        private List<Object> objects = new List<Object>();
        private static GameManager Singleton { get; set; }

        public GameManager()
        {
            if (Singleton == null)
                Singleton = this;
        }

        public void Start()
        {
            Name = "";
            InitializeMap();
            Snake.Start(Map.StartPoint);
            AddWalls();
            Loop();
        }
        public void AddObject(Object obj)
           => Singleton.objects.Add(obj);
        public Object GetObject(Vector2D position)
        {
            foreach (Object obj in Singleton.objects)
                if (obj.Position == position)
                    return obj;
            return null;
        }
        public void RemoveObject(Object obj)
            => Singleton.objects.Remove(obj);

        private void AddScoreToTable(Score score)
        {
            JsonManager json = new JsonManager();
            ScoresFile scoresFile = json.Read<ScoresFile>(GameConfig.ScoresFile);
            if (scoresFile != null)
            {
                scoresFile.AddScores(score);
                json.Write(GameConfig.ScoresFile, scoresFile);
            }
        }
        private void AddWalls()
        {
            ConsoleConfig config = new ConsoleConfig();
            for (int x = 0; x < config.GetBufferX(); x++)
            {
                Object wallUp = new Object("Wall", new Vector2D(x, 0), '#', ConsoleColor.White, true);
                Object wallDown = new Object("Wall", new Vector2D(x, config.GetBufferY() - 2), '#', ConsoleColor.White, true);
            }
            for (int y = 0; y < config.GetBufferY() - 1; y++)
            {
                Object wallLeft = new Object("Wall", new Vector2D(0, y), '#', ConsoleColor.White, true);
                Object wallRight = new Object("Wall", new Vector2D(config.GetBufferX() - 1, y), '#', ConsoleColor.White, true);
            }
        }
        private Object FindObject(string name)
        {
            foreach (Object coor in objects)
                if (coor.Name == name)
                    return coor;
            return null;
        }
        private void GameOver()
        {
            ConsoleConfig config = new ConsoleConfig();
            config.Resize(config.Widht, config.Height);
            int scores = Snake.Scores;
            GameOverGetName(scores);
            GameOverDispose();
            Score score = new Score(scores, Name);
            AddScoreToTable(score);
        }
        private void GenerateApple()
        {
            ConsoleConfig config = new ConsoleConfig();
            bool isLoop = true;
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            do
            {
                x = rnd.Next(1, config.GetBufferX() - 1);
                y = rnd.Next(1, config.GetBufferY() - 1);
                if (GetObject(new Vector2D(x, y)) == null)
                    isLoop = false;
            } while (isLoop);
            Object apple = new Object("Apple", new Vector2D(x, y), '@', ConsoleColor.Red, false);
        }
        private void GameOverDispose()
        {
            Snake.Close();
            for (int i = 0; i < objects.Count; i++)
                objects[i].Destroy();
            objects = new List<Object>();
        }
        private void GameOverGetName(int scores)
        {
            KeyboardControl.PressKeyEvent += OnPressKey;
            KeyboardControl.KeyboardCloseEvent += OnClosingKeyboard;

            MenuManager.Singleton.GameOverMenu(scores);

            KeyboardControl.PressKeyEvent -= OnPressKey;
            KeyboardControl.KeyboardCloseEvent -= OnClosingKeyboard;
        }
        private void InitializeMap()
        {
            ConsoleConfig config = new ConsoleConfig();
            config.Resize(Map.Size.X, Map.Size.Y);
            objects.AddRange(Map.Objects);
        }
        private void Loop()
        {
            render.Clear();
            bool isRunning = true;
            Render();
            do
            {
                if (FindObject("Apple") == null)
                    GenerateApple();
                bool isMove = Snake.Move();
                if(!isMove)
                    isRunning = false;
                Thread.Sleep(RefreshTime);
            } while (isRunning);
            GameOver();
        }
        private void OnPressKey(ConsoleKey key)
        {
            if (WaitForPlayerName)
            {
                switch (key)
                {
                    case ConsoleKey.Enter:
                        WaitForPlayerName = false;
                        break;
                    case ConsoleKey.Backspace:
                        if (Name.Length > 0)
                            Name = Name.Remove(Name.Length - 1, 1);
                        break;
                    case ConsoleKey.Spacebar:
                        if (Name.Length < 11)
                            Name += " ";
                        break;
                    default:
                        {
                            if (Name.Length < 11 && key.ToString().Length == 1)
                                Name += key.ToString();
                            break;
                        }
                }
            }
        }
        private void OnClosingKeyboard()
        {
            KeyboardControl.PressKeyEvent -= OnPressKey;
            KeyboardControl.KeyboardCloseEvent -= OnClosingKeyboard;
        }
        private void Render()
        {
            foreach(Object obj in objects)
            {
                if(obj.CharRender != ' ')
                    render.Write(obj.CharRender+"", obj.Color, obj.Position.X, obj.Position.Y);
            }
        }
    }
}
