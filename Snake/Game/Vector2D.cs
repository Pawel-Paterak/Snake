namespace Snake.Game
{
    public class Vector2D
    {
        public int x { get; set; }
        public int y { get; set; }

        public Vector2D()
        {

        }

        public Vector2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Vector2D l, Vector2D r)
            => (l.x == r.x && l.y == r.y);

        public static bool operator !=(Vector2D l, Vector2D r)
            => (l.x != r.x || l.y != r.y);

        public static Vector2D operator +(Vector2D l, Vector2D r)
           => new Vector2D(l.x+r.x, l.y+r.y);

        public static Vector2D operator -(Vector2D l, Vector2D r)
           => new Vector2D(l.x - r.x, l.y - r.y);
    }
}
