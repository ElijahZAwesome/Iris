using SFML.Graphics;
using SFML.System;

namespace Iris.Graphics
{
    public class RenderContext
    {
        private RenderWindow DefaultTarget { get; }
        private RenderTarget Target { get; set; }
        private PixelShader UsedShader { get; set; }

        internal RenderContext(RenderWindow defaultTarget)
        {
            DefaultTarget = defaultTarget;
            Target = DefaultTarget;
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
            
            Target.Draw(rectShape);
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
            
            Target.Draw(rectShape);
        }

        public void Clear(Color color)
        {
            Target.Clear(color);
        }

        public void Draw(Sprite sprite)
        {
            Target.Draw(sprite.SfmlSprite);
        }

        public void UseShader(PixelShader shader)
        {
            UsedShader = shader;
            Shader.Bind(UsedShader?.SfmlShader);
        }

        public void SetTarget(RenderTarget target)
            => Target = target ?? DefaultTarget;
    }
}