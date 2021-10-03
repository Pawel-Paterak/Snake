using Snake.Configurations;
using Snake.Game.Enums;
using Snake.Game.Managers;
using System;

namespace Snake.Game
{
    public class GenerateObject
    {
        public void GenerateWalls()
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

        public void GenerateApple()
        {
            ConsoleConfig config = new ConsoleConfig();
            GameManager gm = new GameManager();
            bool isLoop = true;
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            do
            {
                x = rnd.Next(1, config.GetBufferX() - 1);
                y = rnd.Next(1, config.GetBufferY() - 1);
                if (gm.GetObject(new Vector2D(x, y)) == null)
                    isLoop = false;
            } while (isLoop);
            GameObject apple = new GameObject("Apple", new Vector2D(x, y), '@', ConsoleColor.Red, GameObjectTagEnum.Object, false);
            apple.Create();
        }
    }
}
