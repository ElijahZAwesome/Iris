namespace Iris.Content.ContentImporters
{
    public abstract class ContentImporter
    {
        internal abstract object ImportObject(string contentPath);
    }

    public abstract class ContentImporter<T> : ContentImporter
    {
        public abstract T Import(string contentPath);
    }
}
