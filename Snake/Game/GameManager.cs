using Snake.Configurations;
using Snake.Controlers;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Render;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake.Game
{
    public class GameManager
    {
        public int RefreshTime { get; set; } = 50;
        public string name { get; private set; } = "";
        public bool waitForPlayerName { get; set; } = false;

        private readonly int offsetLeftWall = 0;
        private readonly int offsetRightWall = 1;
        private readonly int offsetUpWall = 0;
        private readonly int offsetDownWall = 2;
        private readonly Snake snake = new Snake();
        private readonly ConsoleRender render = new ConsoleRender();
        private static List<Object> objects = new List<Object>();

        public void Start()
        {
            name = "";
            snake.Start();
            AddWalls();
            Loop();
        }

        private void Loop()
        {
            bool isRunning = true;
            do
            {
                if (FindObject("apple") == null)
                    GenerateApple();
                bool isMove = snake.Move();
                if(!isMove)
                    isRunning = false;
                Render();
                Thread.Sleep(RefreshTime);
                render.Clear();
            } while (isRunning);
            GameOver();
        }

        private void GameOver()
        {
            int scores = snake.Scores;
            GameOverGetName(scores);
            GameOverDispose();
            Score score = new Score(scores, name);
            VeryficationScore(score);
        }

        private void GameOverGetName(int scores)
        {
            KeyboardControl.PressKeyEvent += OnPressKey;
            KeyboardControl.KeyboardCloseEvent += OnClosingKeyboard;

            Menu menu = new Menu(this);
            menu.GameoverMenu(scores);

            KeyboardControl.PressKeyEvent -= OnPressKey;
            KeyboardControl.KeyboardCloseEvent -= OnClosingKeyboard;
        }

        private void GameOverDispose()
        {
            snake.Close();

            Object apple = FindObject("apple");
            if (apple != null)
                apple.Destroy();

            for (int i = 0; i < objects.Count; i++)
                objects[i].Destroy();
            objects = new List<Object>();
        }

        private void VeryficationScore(Score score)
        {
            JsonManager json = new JsonManager();
            ScoresFile scoresFile = json.Read<ScoresFile>("scores.json");
            if (scoresFile != null)
            {
                int countScores = 17;
                int missingScore = countScores - scoresFile.Scores.Count;
                if (missingScore > 0)
                {
                    for (int i = 0; i < missingScore; i++)
                        scoresFile.Scores.Add(new Score(0, ""));
                }

                for (int i = countScores - 1; i >= 0; i--)
                {
                    if (i == 0 && scoresFile.Scores[i].Scores < score.Scores)
                    {
                        AddScoreToScores(score, i);
                        return;
                    }
                    else if (i != countScores - 1 && scoresFile.Scores[i].Scores >= score.Scores)
                    {
                        AddScoreToScores(score, i + 1);
                        return;
                    }
                }
            }
        }

        private void AddScoreToScores(Score score, int index)
        {
            JsonManager json = new JsonManager();
            ScoresFile scoresFile = json.Read<ScoresFile>("scores.json");
            if (scoresFile != null)
            {
                int countScores = scoresFile.Scores.Count;
                for (int i = countScores - 1; i > index; i--)
                        scoresFile.Scores[i] = scoresFile.Scores[i - 1];

                if(scoresFile.Scores[index].Scores != score.Scores)
                    scoresFile.Scores[index] = score;
                json.Write("scores.json", scoresFile);
            }
        }

        private void AddWalls()
        {
            ConsoleConfiguration console = new ConsoleConfiguration();
            for (int x = 0; x < console.widht; x++)
            {
                Object wallUp = new Object("Wall", new Vector2D(x, offsetUpWall), '#', ConsoleColor.White, true);
                Object wallDown = new Object("Wall", new Vector2D(x, console.height - offsetDownWall), '#', ConsoleColor.White, true);
            }
            for (int y = 0; y < console.height-1; y++)
            {
                Object wallLeft = new Object("Wall", new Vector2D(offsetLeftWall, y), '#', ConsoleColor.White, true);
                Object wallRight = new Object("Wall", new Vector2D(console.widht - offsetRightWall, y), '#', ConsoleColor.White, true);
            }
        }

        private void GenerateApple()
        {
            ConsoleConfiguration console = new ConsoleConfiguration();
            bool isLoop = true;
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            do
            {
                x = rnd.Next(1, console.widht - 1);
                y = rnd.Next(1, console.height - 1);
                if (GetObject(new Vector2D(x, y)) == null)
                    isLoop = false;
            } while (isLoop);
            new Object("apple", new Vector2D(x, y), '@', ConsoleColor.Red, false);
        }

        private void Render()
        {
            foreach(Object obj in objects)
            {
                if(obj.charRender != ' ')
                    render.Write(obj.charRender+"", obj.color, obj.position.x, obj.position.y);
            }
        }

        private Object FindObject(string name)
        {
            foreach (Object coor in objects)
                if (coor.name == name)
                    return coor;
            return null;
        }

        private Object[] FindObjects(string name)
        {
            List<Object> objs = new List<Object>();
            foreach (Object coor in objects)
                if (coor.name == name)
                    objs.Add(coor);
            return objs.ToArray();
        }

        private void OnPressKey(ConsoleKey key)
        {
            if (waitForPlayerName)
            {
                switch(key)
                {
                    case ConsoleKey.Enter:
                            waitForPlayerName = false;
                            break;
                    case ConsoleKey.Backspace:
                        if(name.Length > 0)
                            name = name.Remove(name.Length-1, 1);
                        break;
                    case ConsoleKey.Spacebar:
                        if(name.Length < 11)
                            name += " ";
                        break;
                    default:
                        {
                            if(name.Length < 11 && key.ToString().Length < 2)
                                name += key.ToString();
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

        public static Object GetObject(Vector2D position)
        {
            foreach (Object obj in objects)
                if (obj.position == position)
                    return obj;
            return null;
        }

        public static void AddObject(Object obj)
           => objects.Add(obj);

        public static void RemoveObject(Object obj)
            => objects.Remove(obj);
    }
}
