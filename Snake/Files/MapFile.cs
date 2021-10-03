using Snake.Configurations;
using Snake.Game;
using System.Collections.Generic;

namespace Snake.Files
{
    public class MapFile
    {
        public string Name { get; set; }
        public Vector2D Size { get; set; }
        public Vector2D StartPoint { get; set; }
        public List<GameObject> Objects { get; set; }

        public MapFile(string name, Vector2D size, Vector2D startPoint, List<GameObject> objects)
        {
            Name = name;
            Size = size;
            StartPoint = startPoint;
            Objects = objects;
        }

        public void Initialize()
        {
            ConsoleConfig config = new ConsoleConfig();
            config.Resize(Size.X, Size.Y);
            foreach (GameObject obj in Objects)
                obj.Create();
        }

        public override string ToString()
            => Name;
    }
}
