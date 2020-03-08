using SFML.Graphics;

namespace Iris
{
    public struct Rectangle
    {
        public float Left;
        public float Top;
        public float Width;
        public float Height;

        public Vector2 Position
        {
            get => new Vector2(Left, Top);
            set
            {
                Left = value.X;
                Top = value.Y;
            }
        }

        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public Rectangle(float left, float top, float width, float height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public Rectangle(Vector2 position, Vector2 size)
        {
            Left = position.X;
            Top = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public bool Intersects(Rectangle other)
        {
            return Left + Width >= other.Left &&
                   Left <= other.Left + other.Width &&
                   Top + Height >= other.Top &&
                   Top <= other.Top + other.Height;
        }

        internal IntRect ToSfmlIntRect()
            => new IntRect((int)Left, (int)Top, (int)Width, (int)Height);

        public static implicit operator Rectangle(IntRect intRect)
            => new Rectangle(intRect.Left, intRect.Top, intRect.Width, intRect.Height);

        public static implicit operator Rectangle(FloatRect floatRect)
            => new Rectangle(floatRect.Left, floatRect.Top, floatRect.Width, floatRect.Height);
    }
}