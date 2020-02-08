using Snake.Game.Enums;
using Snake.Game.Render;
using System;

namespace Snake.Game
{
    public class GameObject
    {
        public string Name { get; set; }
        public Vector2D Position { get; private set; }
        public ConsoleColor Color { get; set; }
        public GameObjectTagEnum Tag { get; private set; }
        public char CharRender { get; set; }
        public bool Collision { get; set; }

        private readonly ConsoleRender render = new ConsoleRender();

        public GameObject(string name, Vector2D position, char charRender, ConsoleColor color, GameObjectTagEnum tag, bool collision)
        {
            Name = name;
            Position = position;
            CharRender = charRender;
            Color = color;
            Tag = tag;
            Collision = collision;
        }

        public void Destroy()
        {
            ClearRender();
            GameManager gm = new GameManager();
            gm.RemoveObject(this);
        }

        public void Create()
        {
            Render();
            GameManager gm = new GameManager();
            gm.AddObject(this);
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
    }
}
