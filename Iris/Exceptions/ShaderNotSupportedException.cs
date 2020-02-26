using System;

namespace Iris.Exceptions
{
    public class ShaderNotSupportedException : Exception
    {
        public ShaderNotSupportedException(string message) : base(message)
        {
        }
    }
}