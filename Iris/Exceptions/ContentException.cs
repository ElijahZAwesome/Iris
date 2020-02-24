using System;

namespace Iris.Exceptions
{
    public class ContentException : Exception
    {
        public ContentException(string message) : base(message) { }
    }
}
