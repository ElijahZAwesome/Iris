using System;

namespace Iris.Exceptions
{
    public class IrisException : Exception
    {
        public IrisException(string message) : base(message) { }
    }
}