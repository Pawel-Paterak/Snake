using Snake.Controlers;
using System;

namespace Snake
{
    public class Core
    {
        private Game game;
        private KeyboardControl keyboardControl = new KeyboardControl();

        public void Start()
        {
            keyboardControl.Start();
            Console.ReadKey();
            keyboardControl.Close();
        }
    }
}
