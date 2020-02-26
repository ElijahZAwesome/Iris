using Iris.Graphics;

namespace Iris.Content.ContentImporters
{
    public class FontImporter : ContentImporter<Font>
    {
        public override Font Import(string contentPath)
            => new Font(contentPath);

        internal override object ImportObject(string contentPath)
            => Import(contentPath);
    }
}
