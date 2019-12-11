using Snake.Game.Render;
using System;
using System.Threading;

namespace Snake.Game
{
    public class GameManager
    {
        private int refreshTime = 50;
        private bool isRunning = true;
        private Snake snake = new Snake();
        private GameRender render = new GameRender();

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
            render.Write("@", snake.HeadPosX, snake.HeadPosY);
        }
    }
}
