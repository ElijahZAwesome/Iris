using SFML.Graphics;
using SFML.System;
using SfmlSprite = SFML.Graphics.Sprite;

namespace Iris.Graphics
{
    public class RenderContext
    {
        private RenderWindow DefaultTarget { get; }
        private RenderTarget Target { get; set; }

        public PixelShader CurrentShader { get; private set; }

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
            => Target.Clear(color);

        public void Draw(Sprite sprite)
            => Target.Draw(sprite.SfmlSprite);

        public void Draw(OffscreenBuffer buffer)
            => Target.Draw(
                new SfmlSprite(buffer.RenderTexture.Texture)
            );

        public void Draw(Spritesheet spritesheet, int cellIndex, Vector2 position, Vector2 scale, Color color)
        {
            spritesheet.Configure(cellIndex, position, scale, color);
            Draw(spritesheet.Sprite);
        }

        public void UseOffscreenBuffer(OffscreenBuffer buffer)
        {
            if (buffer == null)
            {
                if (Target != null)
                    (Target as RenderTexture).Display();

                Target = DefaultTarget;
                return;
            }

            Target = buffer.RenderTexture;
        }

        public void UsePixelShader(PixelShader shader)
        {
            CurrentShader = shader;
            Shader.Bind(shader?.SfmlShader);
        }
    }
}