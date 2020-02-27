using SFML.Graphics;
using SfmlSprite = SFML.Graphics.Sprite;

namespace Iris.Graphics
{
    public class OffscreenBuffer
    {
        internal RenderTexture RenderTexture { get; }
        internal SfmlSprite Sprite { get; }

        public OffscreenBuffer(uint width, uint height)
        {
            RenderTexture = new RenderTexture(width, height);
            Sprite = new SfmlSprite(RenderTexture.Texture);
        }
    }
}
