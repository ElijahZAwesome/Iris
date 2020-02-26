namespace Iris.Graphics
{
    public class OpenGlVersion
    {
        public uint Major { get; }
        public uint Minor { get; }
        
        public static readonly OpenGlVersion OpenGl20 = new OpenGlVersion(2, 0);
        public static readonly OpenGlVersion OpenGl21 = new OpenGlVersion(2, 1);
        public static readonly OpenGlVersion OpenGl30 = new OpenGlVersion(3, 0);
        public static readonly OpenGlVersion OpenGl31 = new OpenGlVersion(3, 1);
        public static readonly OpenGlVersion OpenGl32 = new OpenGlVersion(3, 2);
        public static readonly OpenGlVersion OpenGl33 = new OpenGlVersion(3, 3);
        public static readonly OpenGlVersion OpenGl40 = new OpenGlVersion(4, 0);
        public static readonly OpenGlVersion OpenGl41 = new OpenGlVersion(4, 1);
        public static readonly OpenGlVersion OpenGl42 = new OpenGlVersion(4, 2);
        public static readonly OpenGlVersion OpenGl43 = new OpenGlVersion(4, 3);
        public static readonly OpenGlVersion OpenGl44 = new OpenGlVersion(4, 4);
        public static readonly OpenGlVersion OpenGl45 = new OpenGlVersion(4, 5);
        public static readonly OpenGlVersion OpenGl46 = new OpenGlVersion(4, 6);

        internal OpenGlVersion(uint major, uint minor)
        {
            Major = major;
            Minor = minor;
        }
    }
}