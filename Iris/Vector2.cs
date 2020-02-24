using System;
using SFML.Graphics.Glsl;
using SFML.System;

namespace Iris
{
    public struct Vector2
    {
        public float X;
        public float Y;

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
        {
            get
            {
                return (float)Math.Sqrt(
                    Math.Pow(X, 2) +
                    Math.Pow(Y, 2)
                );
            }
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static float Distance(Vector2 a, Vector2 b)
            => (a - b).Magnitude;

        public static Vector2 operator*(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X * right.X,
                left.Y * right.Y
            );
        }

        public static Vector2 operator+(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X + right.X,
                left.Y + right.Y
            );
        }

        public static Vector2 operator-(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X - right.X,
                left.Y - right.Y
            );
        }

        public static Vector2 operator*(Vector2 left, float right)
            => new Vector2(left.X * right, left.Y * right);

        internal Vector2f ToSfmlVector()
            => new Vector2f(X, Y);

        internal Vec2 ToGlslVector()
            => new Vec2(X, Y);
    }
}