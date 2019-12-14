using Snake.Game.Render;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake.Game
{
    public class GameManager
    {
        private int refreshTime = 50;
        private bool isRunning = true;
        private readonly Snake snake = new Snake();
        private readonly GameRender render = new GameRender();
        private static List<Object> objects = new List<Object>();

        public void Start(int refreshTime)
        {
            this.refreshTime = refreshTime;
            snake.Start();
            Loop();
        }

        private void Loop()
        {
            do
            {
                bool isMove = snake.Move();
                if(!isMove)
                    isRunning = false;
                Render();
                Thread.Sleep(refreshTime);
                Console.Clear();
            } while (isRunning);
        }

        private void Render()
        {
            foreach(Object obj in objects)
            {
                if(obj.charRender != ' ')
                    render.Write(obj.charRender+"", obj.position.x, obj.position.y);
            }
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
