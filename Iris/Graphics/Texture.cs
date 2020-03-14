using System;
using System.IO;
using Iris.Exceptions;
using SfmlImage = SFML.Graphics.Image;
using SfmlTexture = SFML.Graphics.Texture;

namespace Iris.Graphics
{
    public class Texture : IDisposable
    {
        internal SfmlImage SfmlImage { get; set; }
        internal SfmlTexture SfmlTexture { get; set; }

        public uint Width => SfmlImage.Size.X;
        public uint Height => SfmlImage.Size.Y;
        public byte[] PixelData => SfmlImage.Pixels;

        public bool Disposed { get; private set; }

        ~Texture()
        {
            Dispose(false);
        }

        public Texture(uint width, uint height)
        {
            SfmlImage = new SfmlImage(width, height);
            SfmlTexture = new SfmlTexture(SfmlImage);
        }

        public Texture(string filePath)
        {
            SfmlImage = new SfmlImage(filePath);
            SfmlTexture = new SfmlTexture(SfmlImage); // Not in there for some reason?
        }

        public Texture(byte[] data)
        {
            SfmlImage = new SfmlImage(data);
        }

        public Texture(Stream stream)
        {
            SfmlImage = new SfmlImage(stream);
        }

        internal Texture(SfmlTexture sfmlTexture)
        {
            SfmlImage = sfmlTexture.CopyToImage();
            SfmlTexture = new SfmlTexture(SfmlImage);
        }

        public void SaveToFile(string filePath)
        {
            EnsureNotDisposed();

            SfmlImage.SaveToFile(filePath);
        }

        public void FlipHorizontal()
        {
            EnsureNotDisposed();

            SfmlImage.FlipHorizontally();
        }

        public void FlipVertical()
        {
            EnsureNotDisposed();

            SfmlImage.FlipVertically();
        }

        public Color GetPixel(uint x, uint y)
        {
            EnsureNotDisposed();

            if (x >= Width || y >= Height)
                throw new CoordinatesOutOfBoundsException(x, y, Width, Height,
                    "Tried to retrieve a pixel outside the texture area.");

            return SfmlImage.GetPixel(x, y);
        }

        public Color GetPixel(Vector2 position)
        {
            EnsureNotDisposed();

            if ((uint)position.X >= Width || (uint)position.Y >= Height)
                throw new CoordinatesOutOfBoundsException((uint)position.X, (uint)position.Y, Width, Height,
                    "Tried to retrieve a pixel outside the texture area.");

            return SfmlImage.GetPixel(
                (uint)position.X,
                (uint)position.Y
            );
        }

        public void SetPixel(uint x, uint y, Color color)
        {
            EnsureNotDisposed();

            if (x >= Width || y >= Height)
                throw new CoordinatesOutOfBoundsException(x, y, Width, Height,
                    "Tried to set a pixel outside the texture area.");

            SfmlImage.SetPixel(x, y, color);
        }

        public void SetPixel(Vector2 position, Color color)
        {
            EnsureNotDisposed();

            if ((uint)position.X >= Width || (uint)position.Y >= Height)
                throw new CoordinatesOutOfBoundsException((uint)position.X, (uint)position.Y, Width, Height,
                    "Tried to set a pixel outside the texture area.");

            SetPixel((uint)position.X, (uint)position.Y, color);
        }

        public void Update()
        {
            EnsureNotDisposed();
            SfmlTexture.Update(SfmlImage);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    // No managed resources to dispose of.
                }

                if(SfmlTexture != null) SfmlTexture.Dispose();
                SfmlImage.Dispose();

                Disposed = true;
            }
        }

        private void EnsureNotDisposed()
        {
            if (Disposed)
                throw new InvalidOperationException("The texture has already been disposed of.");
        }
    }
}