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

        private void PreserveColorCall(Action action)
        {
            var prevColor = Renderer.DrawColor;

            action?.Invoke();
            
            Renderer.DrawColor = prevColor;
        }
    }
}