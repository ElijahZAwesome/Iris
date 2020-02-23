using System;
using SdlSharp;
using SdlSharp.Graphics;

namespace Iris.Graphics
{
    public sealed class RenderContext
    {
        private Renderer Renderer { get; }

        internal RenderContext(Renderer renderer)
        {
            Renderer = renderer;
        }

        public void DrawRectangle(RectangleF rectangle, Color color)
        {
            PreserveColorCall(() =>
            {
                Renderer.DrawColor = color;
                Renderer.DrawRectangle(rectangle);
            });
        }

        public void DrawRectangle(float x, float y, float width, float height, Color color)
        {
            PreserveColorCall(() =>
            {
                Renderer.DrawColor = color;
                Renderer.DrawRectangle(
                    new RectangleF(
                        new PointF(x, y),
                        new SizeF(width, height)
                    )
                );
            });
        }

        public void FillRectangle(RectangleF rectangle, Color color)
        {
            PreserveColorCall(() =>
            {
                Renderer.DrawColor = color;
                Renderer.FillRectangle(rectangle);
            });
        }

        public void FillRectangle(float x, float y, float width, float height, Color color)
        {
            PreserveColorCall(() =>
            {
                Renderer.DrawColor = color;
                Renderer.FillRectangle(
                    new RectangleF(
                        new PointF(x, y),
                        new SizeF(width, height)
                    )
                );
            });
        }

        public void Clear(Color color)
        {
            Renderer.DrawColor = color;
            Renderer.Clear();
        }

        public void Draw(Sprite sprite)
        {
            if (sprite.RenderTexture == null)
            {
                switch (sprite.ScalingMode)
                {
                    case SpriteScalingMode.NearestNeighbor:
                        Hint.RenderScaleQuality.Set("0", HintPriority.Override);
                        break;

                    case SpriteScalingMode.Linear:
                        Hint.RenderScaleQuality.Set("1", HintPriority.Override);
                        break;

                    case SpriteScalingMode.Anisotropic:
                        Hint.RenderScaleQuality.Set("2", HintPriority.Override);
                        break;
                }

                sprite.RenderTexture = Renderer.CreateTexture(sprite.LoadedSurface);
                sprite.RenderTexture.ColorMod = (sprite.Color.Red, sprite.Color.Green, sprite.Color.Blue);
            }

            var targetRect = new RectangleF(
                sprite.RectangleF.Location,
                new SizeF(
                    sprite.RectangleF.Size.Width * sprite.Scale,
                    sprite.Rectangle.Size.Height * sprite.Scale
                )
            );

            Renderer.Copy(
                sprite.RenderTexture,
                null,
                targetRect
            );
        }

        private void PreserveColorCall(Action action)
        {
            var prevColor = Renderer.DrawColor;

            action?.Invoke();

            Renderer.DrawColor = prevColor;
        }
    }
}