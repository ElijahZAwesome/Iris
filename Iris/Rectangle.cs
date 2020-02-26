using SFML.Graphics;

namespace Iris
{
    public struct Rectangle
    {
        public float Left;
        public float Top;
        public float Width;
        public float Height;

        public Rectangle(float left, float top, float width, float height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public bool Intersect(Rectangle other)
        {
            return Left + Width >= other.Left &&
                   Left <= other.Left + other.Width &&
                   Top + Height >= other.Top &&
                   Top <= other.Top + other.Height;
        }
        
        internal IntRect ToSfmlIntRect()
            => new IntRect((int)Left, (int)Top, (int)Width, (int)Height);
    }
}