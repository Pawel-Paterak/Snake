using Snake.Configurations;
using Snake.Controlers;
using Snake.Files;
using Snake.Game.Menu;
using Snake.Game.Render;
using Snake.Game.Settings;
using System;
using System.Threading;

namespace Snake.Game.Managers
{
    public class GameManager
    {
        public int RefreshTime { get; set; } = 50;
        public bool WaitForPlayerName { get; set; }
        public Snake Snake { get; private set; } = new Snake();

        private readonly ConsoleRender render = new ConsoleRender();
        private readonly WorldManager world = new WorldManager();
        private readonly GenerateObject generateObject = new GenerateObject();
        private static GameManager singleton;

        public GameManager()
        {
            if (singleton == null)
                singleton = this;
        }

        public void Start()
        {
            GameSettings.PlayerName = "";
            world.Map.Initialize();
            Snake.Start(world.Map.StartPoint);
            generateObject.GenerateWalls();
            Loop();
        }

        public void AddObject(GameObject obj)
           => singleton.world.AddObject(obj);

        public GameObject GetObject(Vector2D position, GameObject notObj = null)
        {
            foreach (GameObject obj in singleton.world.Objects)
                if (obj.Position == position)
                    if(notObj == null || obj != notObj)
                        return obj;
            return null;
        }

        public GameObject FindObject(string name)
        {
            foreach (GameObject coor in singleton.world.Objects)
                if (coor.Name == name)
                    return coor;
            return null;
        }

        public void RemoveObject(GameObject obj)
            => singleton.world.Objects.Remove(obj);

        public bool CheckPositionForCollision(Vector2D position)
        {
            GameObject obj = GetObject(position);
            if (obj == null)
                return false;

            if (obj.Collision)
                return true;
            return false;
        }

        public void SetMap(MapFile map)
            => world.Map = map;

        private void GameOver()
        {
            ConsoleConfig config = new ConsoleConfig();
            config.Resize(config.Widht, config.Height);
            int scores = Snake.Scores;
            GameOverGetName(scores);
            GameDispose();
            Score score = new Score(scores, GameSettings.PlayerName);
            FileManager file = new FileManager();
            file.NewScore(score);
        }

        private void GameDispose()
        {
            Snake.Close();
            world.DisposeWorld();
        }

        private void GameOverGetName(int scores)
        {
            KeyboardControl.PressKeyEvent += OnPressKey;
            KeyboardControl.KeyboardCloseEvent += OnClosingKeyboard;

            MenuManager.Singleton.GameOverMenu(scores);

            KeyboardControl.PressKeyEvent -= OnPressKey;
            KeyboardControl.KeyboardCloseEvent -= OnClosingKeyboard;
        }

        private void Loop()
        {
            render.Clear();
            bool isRunning = true;
            world.RenderWorld();
            do
            {
                if (FindObject("Apple") == null)
                   generateObject.GenerateApple();
                bool isMove = Snake.Move();
                if(!isMove)
                    isRunning = false;
                world.RenderWorld(false);
                Thread.Sleep(RefreshTime);
            } while (isRunning);
            GameOver();
        }

        private void OnPressKey(ConsoleKey key)
        {
            string name = GameSettings.PlayerName;
            if (WaitForPlayerName)
            {
                switch (key)
                {
                    case ConsoleKey.Enter:
                        WaitForPlayerName = false;
                        break;
                    case ConsoleKey.Backspace:
                        if (name.Length > 0)
                            name = name.Remove(name.Length - 1, 1);
                        break;
                    case ConsoleKey.Spacebar:
                        if (name.Length < 11)
                            name += " ";
                        break;
                    default:
                        {
                            if (name.Length < 11 && key.ToString().Length == 1)
                                name += key.ToString();
                            break;
                        }
                }
            }
            GameSettings.PlayerName = name;
        }

        private void OnClosingKeyboard()
        {
            KeyboardControl.PressKeyEvent -= OnPressKey;
            KeyboardControl.KeyboardCloseEvent -= OnClosingKeyboard;
        }
    }
}
