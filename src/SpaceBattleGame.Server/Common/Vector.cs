namespace SpaceBattleGame.Server.Common
{
    public class Vector
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            if (a == null)
                a = new Vector(0, 0);
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Vector);
        }

        public bool Equals(Vector other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            if (this.X == other.X && this.Y == other.Y)
                return true;
            else
                return false;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            if (ReferenceEquals(v1, v2))
                return true;
            if (ReferenceEquals(v1, null))
                return false;
            if (ReferenceEquals(v2, null))
                return false;
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !v1.Equals(v2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }
    }
}
