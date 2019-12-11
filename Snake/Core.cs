using Snake.Game;
using System;

namespace Snake
{
    public class Core
    {
        private GameManager game = new GameManager();

        public void Start()
        {
            game.Start(100);
            Console.ReadKey();
        }
    }
}
