using SFML.Graphics;
using SFML.System;

namespace Iris.Graphics
{
    public class RenderContext
    {
        private RenderWindow Window { get; }

        public RenderContext(RenderWindow window)
        {
            Window = window;
        }

        public void DrawRectangle(float x, float y, float width, float height, Color color, float thickness = 1.0f)
        {
            var rectShape = new RectangleShape
            {
                Position = new Vector2f(x, y),
                Size = new Vector2f(width, height),
                OutlineColor = color,
                OutlineThickness = thickness,
                FillColor = Color.Transparent
            };
            
            Window.Draw(rectShape);
        }

        public void FillRectangle(float x, float y, float width, float height, Color color)
        {
            var rectShape = new RectangleShape
            {
                Position = new Vector2f(x, y),
                Size = new Vector2f(width, height),
                FillColor = color,
                OutlineColor = Color.Transparent,
                OutlineThickness = 0
            };
            
            Window.Draw(rectShape);
        }

        public void Draw(Sprite sprite)
        {
            Window.Draw(sprite.SfmlSprite);
        }
    }
}