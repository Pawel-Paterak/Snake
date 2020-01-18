using Snake.Game;
using System.Collections.Generic;

namespace Snake.Files
{
    public class MapFile
    {
        public string Name { get; set; }
        public Vector2D Size { get; set; }
        public Vector2D StartPoint { get; set; }
        public List<Object> Objects { get; set; }

        public MapFile()
        {

        }
        public MapFile(string name, Vector2D size, Vector2D startPoint, List<Object> objects)
        {
            Name = name;
            Size = size;
            StartPoint = startPoint;
            Objects = objects;
        }
    }
}
