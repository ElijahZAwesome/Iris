using Iris.Graphics;

namespace Iris.Content.ContentImporters
{
    internal class SpriteImporter : ContentImporter<Sprite>
    {
        public override Sprite Import(string contentPath)
            => new Sprite(contentPath);

        internal override object ImportObject(string contentPath)
            => Import(contentPath);
    }
}
