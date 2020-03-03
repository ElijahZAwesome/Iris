namespace Iris.Exceptions
{
    public class CoordinatesOutOfBoundsException : IrisException
    {
        public uint X { get; }
        public uint Y { get; }

        public uint XLimit { get; }
        public uint YLimit { get; }

        public CoordinatesOutOfBoundsException(uint x, uint y, uint xLimit, uint yLimit, string message) : base(message)
        {
            X = x;
            Y = y;
            XLimit = xLimit;
            YLimit = yLimit;
        }
    }
}