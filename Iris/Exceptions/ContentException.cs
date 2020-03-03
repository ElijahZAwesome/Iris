using System;

namespace Iris.Exceptions
{
    public class ContentException : IrisException
    {
        public ContentException(string message) : base(message) { }
    }
}
