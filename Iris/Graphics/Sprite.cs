using Iris.Internal;
using SFML.Graphics;
using SfmlSprite = SFML.Graphics.Sprite;

namespace Iris.Graphics
{
    public class Sprite
    {
        internal Texture RenderTexture { get; set; }
        internal SfmlSprite SfmlSprite { get; set; }

        public Vector2 Position
        {
            get => SfmlSprite.Position.ToIrisVector();
            set => SfmlSprite.Position = value.ToSfmlVector();
        }

        public Vector2 Origin
        {
            get => SfmlSprite.Origin.ToIrisVector();
            set => SfmlSprite.Origin = value.ToSfmlVector();
        }

        public Vector2 Scale
        {
            get => SfmlSprite.Scale.ToIrisVector();
            set => SfmlSprite.Scale = value.ToSfmlVector();
        }

        public float Rotation
        {
            get => SfmlSprite.Rotation;
            set => SfmlSprite.Rotation = value;
        }
        
        public float Width => RenderTexture.Size.X;
        public float Height => RenderTexture.Size.Y;

        public float ActualWidth => Width * Scale.X;
        public float ActualHeight => Height * Scale.Y;

        public Quad TextureQuad
        {
            get => SfmlSprite.TextureRect.ToIrisQuad();
            set => SfmlSprite.TextureRect = value.ToSfmlIntRect();
        }

        public Color Color { get; set; } = Colors.White;

        public Sprite(string filePath)
        {
            RenderTexture = new Texture(filePath);
            SfmlSprite = new SfmlSprite(RenderTexture);
        }

        public virtual void Draw(RenderContext renderContext)
        {
            renderContext.Draw(this);
        }
    }
}
