using System.IO;
using SfmlImage = SFML.Graphics.Image;

namespace Iris.Graphics
{
    public class Texture
    {
        internal SfmlImage SfmlImage { get; set; }

        public uint Width => SfmlImage.Size.X;
        public uint Height => SfmlImage.Size.Y;
        public byte[] PixelData => SfmlImage.Pixels;

        public Texture(uint width, uint height)
        {
            SfmlImage = new SfmlImage(width, height);
        }

        public Texture(string filePath)
        {
            SfmlImage = new SfmlImage(filePath);
        }

        public Texture(byte[] data)
        {
            SfmlImage = new SfmlImage(data);
        }

        public Texture(Stream stream)
        {
            SfmlImage = new SfmlImage(stream);
        }

        public void SaveToFile(string filePath)
            => SfmlImage.SaveToFile(filePath);

        public void FlipHorizontal()
            => SfmlImage.FlipHorizontally();

        public void FlipVertical()
            => SfmlImage.FlipVertically();

        public Color GetPixel(Vector2 position)
            => SfmlImage.GetPixel(
                (uint)position.X,
                (uint)position.Y
            );

        public void SetPixel(uint x, uint y, Color color)
            => SfmlImage.SetPixel(x, y, color);

        public void SetPixel(Vector2 position, Color color)
            => SetPixel((uint)position.X, (uint)position.Y, color);
    }
}
