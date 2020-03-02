using SFML.Graphics.Glsl;
using System;

namespace Iris
{
    public struct Vector4 : IEquatable<Vector4>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;
        
        public static readonly Vector4 Zero = new Vector4(0, 0, 0, 0);

        public Vector4 Normalized
        {
            get
            {
                var mag = Magnitude;
                return new Vector4(X / mag, Y / mag, Z / mag, W / mag);
            }
        }

        public float Magnitude
            => (float)Math.Sqrt(
                Math.Pow(X, 2) +
                Math.Pow(Y, 2) +
                Math.Pow(Z, 2) +
                Math.Pow(W, 2)
            );

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float DistanceTo(Vector4 target)
            => Distance(this, target);
        
        public bool Equals(Vector4 other)
            => X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);

        public override bool Equals(object obj)
            => obj is Vector4 other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(X, Y, Z, W);

        public static float Distance(Vector4 a, Vector4 b)
            => (a - b).Magnitude;

        public static Vector4 operator *(Vector4 left, Vector4 right)
        {
            return new Vector4(
                left.X * right.X,
                left.Y * right.Y,
                left.Z * right.Z,
                left.W * right.W
            );
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(
                left.X + right.X,
                left.Y + right.Y,
                left.Z + right.Z,
                left.W + right.W
            );
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(
                left.X - right.X,
                left.Y - right.Y,
                left.Z - right.Z,
                left.W - right.W
            );
        }

        public static Vector4 operator *(Vector4 left, float right)
            => new Vector4(left.X * right, left.Y * right, left.Z * right, left.W * right);

        public static Vector4 operator /(Vector4 left, float right)
            => new Vector4(left.X / right, left.Y / right, left.Z / right, left.W / right);

        public static bool operator ==(Vector4 left, Vector4 right)
            => left.X.Equals(right.X) &&
               left.Y.Equals(right.Y) &&
               left.Z.Equals(right.Z) &&
               left.W.Equals(right.W);

        public static bool operator !=(Vector4 left, Vector4 right)
            => !(left == right);

        public static implicit operator Vec4(Vector4 vector)
            => new Vec4(vector.X, vector.Y, vector.Z, vector.W);
    }
}