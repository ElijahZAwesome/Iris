using Iris.Internal;
using SfmlTexture = SFML.Graphics.Texture;
using SfmlSprite = SFML.Graphics.Sprite;

namespace Iris.Graphics
{
    public class Sprite
    {
        internal SfmlTexture SfmlTexture { get; set; }
        internal SfmlSprite SfmlSprite { get; set; }

        public Vector2 Position
        {
            get => SfmlSprite.Position.ToIrisVector();
            set => SfmlSprite.Position = value;
        }

        public Vector2 Origin
        {
            get => SfmlSprite.Origin.ToIrisVector();
            set => SfmlSprite.Origin = value;
        }

        public Vector2 Scale
        {
            get => SfmlSprite.Scale.ToIrisVector();
            set => SfmlSprite.Scale = value;
        }

        public float Rotation
        {
            get => SfmlSprite.Rotation;
            set => SfmlSprite.Rotation = value;
        }
        
        public float Width => SfmlTexture.Size.X;
        public float Height => SfmlTexture.Size.Y;

        public float ActualWidth => Width * Scale.X;
        public float ActualHeight => Height * Scale.Y;

        public Rectangle SourceRectangle
        {
            get => SfmlSprite.TextureRect.ToIrisRectangle();
            set => SfmlSprite.TextureRect = value.ToSfmlIntRect();
        }

        public Texture Texture { get; set; }
        public Color Color { get; set; } = Color.White;

        internal Sprite(string filePath)
        {
            Texture = new Texture(filePath);

            SfmlTexture = new SfmlTexture(Texture.SfmlImage);
            SfmlSprite = new SfmlSprite(SfmlTexture);
        }

        public Sprite(Texture texture)
        {
            Texture = texture;

            SfmlTexture = new SfmlTexture(Texture.SfmlImage);
            SfmlSprite = new SfmlSprite(SfmlTexture);
        }

        public void UpdateTexture()
            => SfmlTexture?.Update(Texture.SfmlImage);
    }
}
