using SdlSharp;
using SdlSharp.Graphics;

namespace Iris.Graphics
{
    public class Sprite
    {
        internal Surface LoadedSurface { get; }
        internal Texture RenderTexture { get; set; }

        public float X { get; set; }
        public float Y { get; set; }

        public float Scale { get; set; } = 1f;
        public SpriteScalingMode ScalingMode { get; set; }

        public int Width => LoadedSurface.Size.Width;
        public int Height => LoadedSurface.Size.Height;

        public float ActualWidth => Width * Scale;
        public float ActualHeight => Height * Scale;

        public Color Color { get; set; } = Colors.White;

        public Rectangle Rectangle => new Rectangle(
            new Point((int)X, (int)Y),
            new Size(Width, Height)
        );

        public RectangleF RectangleF => new RectangleF(
            new PointF(X, Y),
            new SizeF(Width, Height)
        );

        public Sprite(string filePath)
        {
            LoadedSurface = Image.Load(filePath);
        }

        public void Draw(RenderContext context)
        {
            context.Draw(this);
        }
    }
}
