using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Game
{
    public class Object
    {
        public string name { get; private set; } = "Default";
        public Vector2D position { get; set; } = new Vector2D(0, 0);
        public ConsoleColor color { get; private set; } = ConsoleColor.White;
        public char charRender { get; private set; } = ' ';
        public bool collision { get; private set; }

        public Object()
        {
            Instantiate();
        }

        public Object(string name, Vector2D position, char charRender, ConsoleColor color, bool collision)
        {
            this.name = name;
            this.position = position;
            this.charRender = charRender;
            this.color = color;
            this.collision = collision;
            Instantiate();
        }

        private void Instantiate()
            => GameManager.AddObject(this);

        public void Destroy()
           => GameManager.RemoveObject(this);
    }
}
