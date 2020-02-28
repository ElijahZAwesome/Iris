using SFML.Graphics.Glsl;
using SFML.System;
using System;

namespace Iris
{
    public struct Vector3 : IEquatable<Vector3>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public static readonly Vector3 Up = new Vector3(0, 1, 0);
        public static readonly Vector3 Down = new Vector3(0, -1, 0);
        public static readonly Vector3 Right = new Vector3(1, 0, 0);
        public static readonly Vector3 Left = new Vector3(-1, 0, 0);
        public static readonly Vector3 Forward = new Vector3(0, 0, 1);
        public static readonly Vector3 Backward = new Vector3(0, 0, -1);
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);

        public Vector3 Normalized
        {
            get
            {
                var mag = Magnitude;
                return new Vector3(X / mag, Y / mag, Z / mag);
            }
        }

        public float Magnitude
            => (float)Math.Sqrt(
                Math.Pow(X, 2) +
                Math.Pow(Y, 2) +
                Math.Pow(Z, 2)
            );

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float DistanceTo(Vector3 target)
            => Distance(this, target);
        
        public bool Equals(Vector3 other)
            => X.Equals(other.X) && 
               Y.Equals(other.Y) && 
               Z.Equals(other.Z);

        public override bool Equals(object obj)
            => obj is Vector3 other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(X, Y, Z);

        public static float Distance(Vector3 a, Vector3 b)
            => (a - b).Magnitude;

        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.X * right.X,
                left.Y * right.Y,
                left.Z * right.Z
            );
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.X + right.X,
                left.Y + right.Y,
                left.Z + right.Z
            );
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.X - right.X,
                left.Y - right.Y,
                left.Z - right.Z
            );
        }

        public static Vector3 operator *(Vector3 left, float right)
            => new Vector3(left.X * right, left.Y * right, left.Z * right);

        public static Vector3 operator /(Vector3 left, float right)
            => new Vector3(left.X / right, left.Y / right, left.Z / right);

        public static bool operator ==(Vector3 left, Vector3 right)
            => left.X.Equals(right.X) &&
               left.Y.Equals(right.Y) &&
               left.Z.Equals(right.Z);

        public static bool operator !=(Vector3 left, Vector3 right) 
            => !(left == right);

        internal Vector3f ToSfmlVector()
            => new Vector3f(X, Y, Z);

        internal Vec3 ToGlslVector()
            => new Vec3(X, Y, Z);
    }
}