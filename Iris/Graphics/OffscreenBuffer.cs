using SFML.Graphics;

namespace Iris.Graphics
{
    public class OffscreenBuffer
    {
        internal RenderTexture RenderTexture { get; }

        public OffscreenBuffer(uint width, uint height)
        {
            RenderTexture = new RenderTexture(width, height);
        }
    }
}
