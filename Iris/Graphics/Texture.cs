using System.IO;
using Iris.Exceptions;
using SfmlImage = SFML.Graphics.Image;
using SfmlTexture = SFML.Graphics.Texture;

namespace Iris.Graphics
{
    public class Texture
    {
        internal SfmlImage SfmlImage { get; set; }
        internal SfmlTexture SfmlTexture { get; set; }

        public uint Width => SfmlImage.Size.X;
        public uint Height => SfmlImage.Size.Y;
        public byte[] PixelData => SfmlImage.Pixels;

        public Texture(uint width, uint height)
        {
            SfmlImage = new SfmlImage(width, height);
            SfmlTexture = new SfmlTexture(SfmlImage);
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

        public Color GetPixel(uint x, uint y)
        {
            if (x >= Width || y >= Height)
                throw new CoordinatesOutOfBoundsException(x, y, Width, Height,
                    "Tried to retrieve a pixel outside the texture area.");

            return SfmlImage.GetPixel(x, y);
        }

        public Color GetPixel(Vector2 position)
        {
            if((uint)position.X >= Width || (uint)position.Y >= Height)
                throw new CoordinatesOutOfBoundsException((uint)position.X, (uint)position.Y, Width, Height,
                    "Tried to retrieve a pixel outside the texture area.");
                
            return SfmlImage.GetPixel(
                (uint)position.X,
                (uint)position.Y
            );
        }

        public void SetPixel(uint x, uint y, Color color)
        {
            if (x >= Width || y >= Height)
                throw new CoordinatesOutOfBoundsException(x, y, Width, Height,
                    "Tried to set a pixel outside the texture area.");

            SfmlImage.SetPixel(x, y, color);
        }

        public void SetPixel(Vector2 position, Color color)
        {
            if ((uint)position.X >= Width || (uint)position.Y >= Height)
                throw new CoordinatesOutOfBoundsException((uint)position.X, (uint)position.Y, Width, Height,
                    "Tried to set a pixel outside the texture area.");

            SetPixel((uint)position.X, (uint)position.Y, color);
        }

        public void Update()
            => SfmlTexture.Update(SfmlImage);
    }
}