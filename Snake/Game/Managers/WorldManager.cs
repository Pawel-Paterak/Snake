using Snake.Files;
using System.Collections.Generic;

namespace Snake.Game.Managers
{
    public class WorldManager
    {
        public MapFile Map { get; set; }
        public List<GameObject> Objects { get; private set; } = new List<GameObject>();

        public void AddObject(GameObject obj)
            => Objects.Add(obj);

        public void RemoveObject(GameObject obj)
            => Objects.Remove(obj);

        public void RenderWorld(bool collisionRender = true)
        {
            foreach (GameObject obj in Objects)
            {
                if (obj.CharRender != ' ')
                {
                    if (!collisionRender == !obj.Collision)
                        obj.Render();
                    else if (collisionRender)
                        obj.Render();
                }
            }
        }

        public void DisposeWorld()
        {
            for (int i = 0; i < Objects.Count; i++)
                Objects[i].Destroy();
            Objects = new List<GameObject>();
        }
    }
}
