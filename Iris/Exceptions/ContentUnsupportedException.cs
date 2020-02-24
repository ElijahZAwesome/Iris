using System;

namespace Iris.Exceptions
{
    public class ContentUnsupportedException : ContentException
    {
        public Type RequestedType { get; }

        public ContentUnsupportedException(Type requestedType, string message)
            : base(message)
        {
            RequestedType = requestedType;
        }
    }
}
