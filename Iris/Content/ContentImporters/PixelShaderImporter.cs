using Iris.Graphics;

namespace Iris.Content.ContentImporters
{
    public class PixelShaderImporter : ContentImporter<PixelShader>
    {
        public override PixelShader Import(string contentPath)
            => new PixelShader(contentPath);

        internal override object ImportObject(string contentPath)
            => Import(contentPath);
    }
}