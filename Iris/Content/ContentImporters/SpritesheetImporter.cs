using Iris.Graphics;

namespace Iris.Content.ContentImporters
{
    public class SpritesheetImporter : ContentImporter<Spritesheet>
    {
        public override Spritesheet Import(string contentPath)
            => new Spritesheet(contentPath);

        internal override object ImportObject(string contentPath)
            => Import(contentPath);
    }
}
