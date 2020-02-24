using SFML.Graphics;
using SfmlSprite = SFML.Graphics.Sprite;

namespace Iris.Graphics
{
    public class Sprite : Drawable
    {
        internal Texture RenderTexture { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; } = new Vector2(1, 1);

        public float Rotation { get; set; }
        
        public float Width => RenderTexture.Size.X;
        public float Height => RenderTexture.Size.Y;

        public float ActualWidth => Width * Scale.X;
        public float ActualHeight => Height * Scale.Y;

        public Color Color { get; set; } = Colors.White;

        public Sprite(string filePath)
        {
            RenderTexture = new Texture(filePath);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            var sfmlSprite = new SfmlSprite
            {
                Color = Color,
                Position = Position.ToSfmlVector(),
                Scale = Scale.ToSfmlVector(),
                Origin = Origin.ToSfmlVector(),
                Texture = RenderTexture,
                Rotation = Rotation
            };
            
            target.Draw(sfmlSprite, states);
        }
    }
}
