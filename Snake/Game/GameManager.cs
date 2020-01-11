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

        private readonly int offsetLeftWall = 0;
        private readonly int offsetRightWall = 1;
        private readonly int offsetUpWall = 0;
        private readonly int offsetDownWall = 2;
        private readonly ConsoleRender render = new ConsoleRender();
        private static List<Object> objects = new List<Object>();

        public void Start()
        {
            Name = "";
            Snake.Start();
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
                bool isMove = Snake.Move();
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
            int scores = Snake.Scores;
            GameOverGetName(scores);
            GameOverDispose();
            Score score = new Score(scores, Name);
            VeryficationScore(score);
        }

        private void GameOverGetName(int scores)
        {
            KeyboardControl.PressKeyEvent += OnPressKey;
            KeyboardControl.KeyboardCloseEvent += OnClosingKeyboard;

            MenuManager.singleton.GameoverMenu(scores);

            KeyboardControl.PressKeyEvent -= OnPressKey;
            KeyboardControl.KeyboardCloseEvent -= OnClosingKeyboard;
        }

        private void GameOverDispose()
        {
            Snake.Close();

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
            ScoresFile scoresFile = json.Read<ScoresFile>(GameConfig.ScoresFile);
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
            ScoresFile scoresFile = json.Read<ScoresFile>(GameConfig.ScoresFile);
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
            ConsoleConfig console = new ConsoleConfig();
            for (int x = 0; x < console.Widht; x++)
            {
                Object wallUp = new Object("Wall", new Vector2D(x, offsetUpWall), '#', ConsoleColor.White, true);
                Object wallDown = new Object("Wall", new Vector2D(x, console.Height - offsetDownWall), '#', ConsoleColor.White, true);
            }
            for (int y = 0; y < console.Height-1; y++)
            {
                Object wallLeft = new Object("Wall", new Vector2D(offsetLeftWall, y), '#', ConsoleColor.White, true);
                Object wallRight = new Object("Wall", new Vector2D(console.Widht - offsetRightWall, y), '#', ConsoleColor.White, true);
            }
        }

        private void GenerateApple()
        {
            ConsoleConfig console = new ConsoleConfig();
            bool isLoop = true;
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            do
            {
                x = rnd.Next(1, console.Widht - 1);
                y = rnd.Next(1, console.Height - 1);
                if (GetObject(new Vector2D(x, y)) == null)
                    isLoop = false;
            } while (isLoop);
            Object apple = new Object("apple", new Vector2D(x, y), '@', ConsoleColor.Red, false);
        }

        private void Render()
        {
            foreach(Object obj in objects)
            {
                if(obj.CharRender != ' ')
                    render.Write(obj.CharRender+"", obj.Color, obj.Position.X, obj.Position.Y);
            }
        }

        private Object FindObject(string name)
        {
            foreach (Object coor in objects)
                if (coor.Name == name)
                    return coor;
            return null;
        }

        private void OnPressKey(ConsoleKey key)
        {
            if (WaitForPlayerName)
            {
                switch(key)
                {
                    case ConsoleKey.Enter:
                            WaitForPlayerName = false;
                            break;
                    case ConsoleKey.Backspace:
                        if(Name.Length > 0)
                            Name = Name.Remove(Name.Length-1, 1);
                        break;
                    case ConsoleKey.Spacebar:
                        if(Name.Length < 11)
                            Name += " ";
                        break;
                    default:
                        {
                            if(Name.Length < 11 && key.ToString().Length < 2)
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

        public static Object GetObject(Vector2D position)
        {
            foreach (Object obj in objects)
                if (obj.Position == position)
                    return obj;
            return null;
        }

        public static void AddObject(Object obj)
           => objects.Add(obj);

        public static void RemoveObject(Object obj)
            => objects.Remove(obj);
    }
}
