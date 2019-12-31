using Snake.Configurations;
using Snake.Game.Render;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake.Game
{
    public class GameManager
    {
        public int RefreshTime { get; set; } = 50;
        private bool isRunning = true;
        private readonly Snake snake = new Snake();
        private readonly ConsoleRender render = new ConsoleRender();
        private static List<Object> objects = new List<Object>();

        public void Start()
        {
            snake.Start();
            Loop();
        }

        private void Loop()
        {
            do
            {
                if (FindObject("apple") == null)
                    GenerateApple();
                bool isMove = snake.Move();
                if(!isMove)
                    isRunning = false;
                Render();
                Thread.Sleep(RefreshTime);
                Console.Clear();
            } while (isRunning);
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
