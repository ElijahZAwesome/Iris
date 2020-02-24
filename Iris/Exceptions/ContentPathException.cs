namespace Iris.Exceptions
{
    public class ContentPathException : ContentException
    {
        public string FailingPath { get; }
        
        public ContentPathException(string failingPath, string message)
            : base(message)
        {
            FailingPath = failingPath;
        }
    }
}
