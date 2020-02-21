using Snake.Configurations;
using Snake.Controlers;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Menu;
using Snake.Game.Render;
using Snake.Game.Enums;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake.Game
{
    public class GameManager
    {
        public int RefreshTime { get; set; } = 50;
        public string Name { get; private set; }
        public bool WaitForPlayerName { get; set; }
        public Snake Snake { get; private set; } = new Snake();
        public MapFile Map { get; set; }

        private readonly ConsoleRender render = new ConsoleRender();
        private List<GameObject> objects = new List<GameObject>();
        private static GameManager Singleton;

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

        public void AddObject(GameObject obj)
           => Singleton.objects.Add(obj);

        public GameObject GetObject(Vector2D position)
        {
            foreach (GameObject obj in Singleton.objects)
                if (obj.Position == position)
                    return obj;
            return null;
        }

        public void RemoveObject(GameObject obj)
            => Singleton.objects.Remove(obj);

        public GameObject FindObject(string name)
        {
            foreach (GameObject coor in Singleton.objects)
                if (coor.Name == name)
                    return coor;
            return null;
        }

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
                GameObject wallUp = new GameObject("Wall", new Vector2D(x, 0), '#', ConsoleColor.White, GameObjectTagEnum.Object, true);
                GameObject wallDown = new GameObject("Wall", new Vector2D(x, config.GetBufferY() - 2), '#', ConsoleColor.White, GameObjectTagEnum.Object, true);
                wallUp.Create();
                wallDown.Create();
            }
            for (int y = 0; y < config.GetBufferY() - 1; y++)
            {
                GameObject wallLeft = new GameObject("Wall", new Vector2D(0, y), '#', ConsoleColor.White, GameObjectTagEnum.Object, true);
                GameObject wallRight = new GameObject("Wall", new Vector2D(config.GetBufferX() - 1, y), '#', ConsoleColor.White, GameObjectTagEnum.Object, true);
                wallLeft.Create();
                wallRight.Create();
            }
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
            GameObject apple = new GameObject("Apple", new Vector2D(x, y), '@', ConsoleColor.Red, GameObjectTagEnum.Object, false);
            apple.Create();
        }

        private void GameOverDispose()
        {
            Snake.Close();
            for (int i = 0; i < objects.Count; i++)
                objects[i].Destroy();
            objects = new List<GameObject>();
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
            foreach (GameObject obj in Map.Objects)
                obj.Create();
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
                Render(false);
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

        private void Render(bool collisionRender = true)
        {
            foreach(GameObject obj in objects)
            {
                if (obj.CharRender != ' ')
                {
                    if(!collisionRender == !obj.Collision)
                        obj.Render();
                    else if(collisionRender)
                        obj.Render();
                }
            }
        }
    }
}
