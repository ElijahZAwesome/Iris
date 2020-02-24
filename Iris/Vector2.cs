using System;
using SFML.System;

namespace Iris
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public Vector2 Normalized()
            => new Vector2(X / Magnitude(), Y / Magnitude());
        
        public float Magnitude()
            => (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        
        internal Vector2f ToSfmlVector()
            => new Vector2f(X, Y);
    }
}