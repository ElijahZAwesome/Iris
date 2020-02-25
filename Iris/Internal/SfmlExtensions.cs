using SFML.Graphics;
using SFML.System;

namespace Iris.Internal
{
    internal static class SfmlExtensions
    {
        internal static Rectangle ToIrisRectangle(this IntRect intRect)
            => new Rectangle(intRect.Left, intRect.Top, intRect.Width, intRect.Height);

        internal static Vector2 ToIrisVector(this Vector2f vec)
            => new Vector2(vec.X, vec.Y);

        internal static Vector2 ToIrisVector(this Vector2i vec)
            => new Vector2(vec.X, vec.Y);

        internal static Vector2 ToIrisVector(this Vector2u vec)
            => new Vector2(vec.X, vec.Y);

        internal static Vector3 ToIrisVector(this Vector3f vec)
            => new Vector3(vec.X, vec.Y, vec.Z);
    }
}