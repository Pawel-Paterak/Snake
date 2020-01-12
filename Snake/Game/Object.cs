using Snake.Game.Render;
using System;

namespace Snake.Game
{
    public class Object
    {
        public string Name { get; set; } = "Default";
        public Vector2D Position { get; private set; } = new Vector2D(0, 0);
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
        public char CharRender { get; set; } = ' ';
        public bool Collision { get; set; } = false;

        private ConsoleRender render = new ConsoleRender();

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
        public void Destroy()
        {
            ClearRender();
            GameManager.RemoveObject(this);
        }
        public void Move(Vector2D position, bool isOffset)
           => Move(position, true, true, isOffset);
        public void Move(Vector2D position, bool render, bool isOffset)
           => Move(position, render, render, isOffset);
        public void Move(Vector2D position, bool render = true, bool clear = true, bool isOffset = false)
        {
            if (clear)
                ClearRender();

            if (isOffset)
                Position += position;
            else
                Position = position;

            if (render)
                Render();
        }
        public void Render()
           => render.Write(CharRender.ToString(), Color, Position.X, Position.Y);
        public void ClearRender()
            => render.Write(" ", Position.X, Position.Y);

        private void Instantiate()
        {
            Render();
            GameManager.AddObject(this);
        }
    }
}
