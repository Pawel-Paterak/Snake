using Snake.Controlers;
using System;

namespace Snake
{
    public class Core
    {
        private Game game = new Game();

        public void Start()
        {
            game.Start();
            Console.ReadKey();
        }
    }
}
