using SFML.Graphics.Glsl;
using System;

namespace Iris
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4 Normalized
        {
            get
            {
                var mag = Magnitude;
                return new Vector4(X / mag, Y / mag, Z / mag, W / mag);
            }
        }

        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(
                    Math.Pow(X, 2) +
                    Math.Pow(Y, 2) +
                    Math.Pow(Z, 2) +
                    Math.Pow(W, 2)
                );
            }
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

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

        internal Vec4 ToGlslVector()
            => new Vec4(X, Y, Z, W);
    }
}
