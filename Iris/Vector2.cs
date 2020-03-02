using System;
using SFML.Graphics.Glsl;
using SFML.System;

namespace Iris
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public readonly float X;
        public readonly float Y;

        public static readonly Vector2 Up = new Vector2(0, -1);
        public static readonly Vector2 Down = new Vector2(0, 1);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Zero = new Vector2(0, 0);

        public Vector2 Normalized
        {
            get
            {
                var mag = Magnitude;
                return new Vector2(X / mag, Y / mag);
            }
        }

        public float Magnitude
            => (float)Math.Sqrt(
                Math.Pow(X, 2) +
                Math.Pow(Y, 2)
            );

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public float DistanceTo(Vector2 target)
            => Distance(this, target);
        
        public bool Equals(Vector2 other)
            => X.Equals(other.X) && 
               Y.Equals(other.Y);
        
        public override bool Equals(object obj)
            => obj is Vector2 other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(X, Y);

        public static float Distance(Vector2 a, Vector2 b)
            => (a - b).Magnitude;

        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X * right.X,
                left.Y * right.Y
            );
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X + right.X,
                left.Y + right.Y
            );
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X - right.X,
                left.Y - right.Y
            );
        }

        public static Vector2 operator *(Vector2 left, float right)
            => new Vector2(left.X * right, left.Y * right);

        public static Vector2 operator /(Vector2 left, float right)
            => new Vector2(left.X * right, left.Y * right);

        public static bool operator ==(Vector2 left, Vector2 right)
            => left.X.Equals(right.X) && left.Y.Equals(right.Y);

        public static bool operator !=(Vector2 left, Vector2 right)
            => !(left == right);

        public static implicit operator Vector2f(Vector2 vector)
            => new Vector2f(vector.X, vector.Y);

        public static implicit operator Vector2i(Vector2 vector)
            => new Vector2i((int)vector.X, (int)vector.Y);

        public static implicit operator Vec2(Vector2 vector)
            => new Vec2(vector.X, vector.Y);
    }
}