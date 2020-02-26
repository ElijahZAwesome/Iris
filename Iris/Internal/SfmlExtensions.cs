using Iris.Graphics;
using SFML.Graphics;
using SFML.System;

namespace Iris.Internal
{
    internal static class SfmlExtensions
    {
        internal static Rectangle ToIrisRectangle(this IntRect intRect)
            => new Rectangle(intRect.Left, intRect.Top, intRect.Width, intRect.Height);

        internal static Rectangle ToIrisRectangle(this FloatRect floatRect)
            => new Rectangle(floatRect.Left, floatRect.Top, floatRect.Width, floatRect.Height);

        internal static Vector2 ToIrisVector(this Vector2f vec)
            => new Vector2(vec.X, vec.Y);

        internal static Vector2 ToIrisVector(this Vector2i vec)
            => new Vector2(vec.X, vec.Y);

        internal static Vector2 ToIrisVector(this Vector2u vec)
            => new Vector2(vec.X, vec.Y);

        internal static Vector3 ToIrisVector(this Vector3f vec)
            => new Vector3(vec.X, vec.Y, vec.Z);

        internal static BlendingMode ToIrisBlendingMode(this BlendMode blendMode)
        {
            if (blendMode == BlendMode.Add)
                return BlendingMode.Add;
            else if (blendMode == BlendMode.Multiply)
                return BlendingMode.Multiply;
            else if (blendMode == BlendMode.Alpha)
                return BlendingMode.Alpha;
            else if (blendMode == BlendMode.None)
                return BlendingMode.None;
            return BlendingMode.Default;
        }

        internal static BlendMode ToSfmlBlendMode(this BlendingMode blendingMode)
        {
            return blendingMode switch
            {
                BlendingMode.None => BlendMode.None,
                BlendingMode.Add => BlendMode.Add,
                BlendingMode.Alpha => BlendMode.Alpha,
                BlendingMode.Multiply => BlendMode.Multiply,
                _ => RenderStates.Default.BlendMode
            };
        }
    }
}