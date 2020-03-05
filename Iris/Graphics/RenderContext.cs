using Iris.Internal;
using SFML.Graphics;

namespace Iris.Graphics
{
    public class RenderContext
    {
        private RenderWindow DefaultTarget { get; }
        private RenderTarget Target { get; set; }
        private RenderStates RenderStates { get; set; } = RenderStates.Default;

        public PixelShader CurrentShader { get; private set; }
        public BlendingMode BlendingMode { get; private set; }

        internal RenderContext(RenderWindow defaultTarget)
        {
            DefaultTarget = defaultTarget;
            Target = DefaultTarget;
            BlendingMode = BlendingMode.Default;
        }

        public void DrawRectangle(Vector2 position, Vector2 size, Vector2 origin, Vector2 scale, Color color, float rotation, float thickness)
        {
            using var rectShape = new RectangleShape
            {
                Position = position,
                Size = size,
                Origin = origin,
                Scale = scale,
                OutlineColor = color,
                Rotation = rotation,
                OutlineThickness = thickness,
                FillColor = Color.Transparent
            };

            Target.Draw(rectShape, RenderStates);
        }

        public void DrawRectangle(float x, float y, float width, float height, Color color, float thickness = 1f)
            => DrawRectangle(
                new Vector2(x, y),
                new Vector2(width, height),
                new Vector2(0, 0),
                new Vector2(1, 1),
                color,
                0, 
                thickness
            );

        public void DrawRectangle(Vector2 position, Vector2 size, Color color, float thickness = 1f)
            => DrawRectangle(
                position,
                size,
                new Vector2(0, 0),
                new Vector2(1, 1),
                color,
                0,
                thickness
            );

        public void FillRectangle(Vector2 position, Vector2 size, Vector2 origin, Vector2 scale, Color color, float rotation)
        {
            using var rectShape = new RectangleShape
            {
                Position = position,
                Size = size,
                Origin = origin,
                Scale = scale,
                FillColor = color,
                Rotation = rotation,
                OutlineColor = Color.Transparent,
                OutlineThickness = 0
            };

            Target.Draw(rectShape, RenderStates);
        }

        public void FillRectangle(float x, float y, float width, float height, Color color)
            => FillRectangle(
                new Vector2(x, y),
                new Vector2(width, height),
                new Vector2(0, 0),
                new Vector2(1, 1),
                color,
                0
            );

        public void FillRectangle(Vector2 position, Vector2 size, Color color)
            => FillRectangle(
                position,
                size,
                new Vector2(0, 0),
                new Vector2(1, 1),
                color,
                0
            );

        public void Clear(Color color)
            => Target.Clear(color);

        public void Draw(Sprite sprite)
            => Target.Draw(sprite.SfmlSprite, RenderStates);

        public void Draw(OffscreenBuffer buffer)
            => Target.Draw(buffer.Sprite, RenderStates);

        public void Draw(Spritesheet spritesheet, int cellIndex, Vector2 position, Vector2 scale, Color color)
        {
            spritesheet.Configure(cellIndex, position, scale, color);
            Draw(spritesheet.Sprite);
        }

        public void DrawString(Font font, string str, Vector2 position, float rotation, Color color)
        {
            var text = font.ConstructText(str);

            text.FillColor = color;
            text.Position = position;
            text.Rotation = rotation;

            Target.Draw(text, RenderStates);
            text.Dispose();
        }

        public void DrawString(Font font, string str, Vector2 position, Color color)
            => DrawString(font, str, position, 0.0f, color);

        public void DrawLine(Vector2 a, Vector2 b, float lineThickness, Color color)
        {
            var line = new LineDrawable(a, b, color, lineThickness);
            Target.Draw(line, RenderStates);
        }

        public void BlendUsing(BlendingMode blendingMode)
        {
            RenderStates = new RenderStates(
                blendingMode.ToSfmlBlendMode(),
                RenderStates.Transform,
                RenderStates.Texture,
                RenderStates.Shader
            );
        }

        public void UseOffscreenBuffer(OffscreenBuffer buffer)
        {
            if (buffer == null)
            {
                (Target as RenderTexture)?.Display();
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