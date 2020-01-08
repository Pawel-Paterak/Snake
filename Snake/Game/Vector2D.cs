namespace Snake.Game
{
    public class Vector2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2D()
        {

        }

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            Vector2D vector = obj as Vector2D;
            return X == vector.X && Y == vector.Y;
        }

        public override int GetHashCode()
        {
           return 0;
        }

        public static bool operator ==(Vector2D l, Vector2D r)
           => l.X == r.X && l.Y == r.Y;

        public static bool operator !=(Vector2D l, Vector2D r)
           => l.X != r.X || l.Y != r.Y;

        public static Vector2D operator +(Vector2D l, Vector2D r)
           => new Vector2D(l.X+r.X, l.Y+r.Y);

        public static Vector2D operator -(Vector2D l, Vector2D r)
           => new Vector2D(l.X - r.X, l.Y - r.Y);
    }
}
