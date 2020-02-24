using SFML.Graphics;

namespace Iris
{
    public struct Quad
    {
        public int Left;
        public int Top;

        public int Width;
        public int Height;

        public Quad(int left, int top, int width, int height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
        
        internal IntRect ToSfmlIntRect()
            => new IntRect(Left, Top, Width, Height);
    }
}