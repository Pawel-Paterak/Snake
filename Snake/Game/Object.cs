using System;

namespace Snake.Game
{
    public class Object
    {
        public string Name { get; private set; } = "Default";
        public Vector2D Position { get; set; } = new Vector2D(0, 0);
        public ConsoleColor Color { get; private set; } = ConsoleColor.White;
        public char CharRender { get; private set; } = ' ';
        public bool Collision { get; private set; }

        public Object()
        {
            Instantiate();
        }

        public Object(string name, Vector2D position, char charRender, ConsoleColor color, bool collision)
        {
            Name = name;
            Position = position;
            CharRender = charRender;
            Color = color;
            Collision = collision;
            Instantiate();
        }

        private void Instantiate()
            => GameManager.AddObject(this);

        public void Destroy()
           => GameManager.RemoveObject(this);
    }
}
