using SFML.Graphics;
using SFML.System;

namespace Iris.Internal
{
    internal static class SfmlExtensions
    {
        internal static Quad ToIrisQuad(this IntRect intRect)
            => new Quad(intRect.Left, intRect.Top, intRect.Width, intRect.Height);

        internal static Vector2 ToIrisVector(this Vector2f vec)
            => new Vector2(vec.X, vec.Y);

        internal static Vector2 ToIrisVector(this Vector2i vec)
            => new Vector2(vec.X, vec.Y);

        internal static Vector2 ToIrisVector(this Vector2u vec)
            => new Vector2(vec.X, vec.Y);
    }
}
