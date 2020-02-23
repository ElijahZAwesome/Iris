using SdlSharp.Graphics;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private int X { get; } = 100;
        private int Y { get; } = 100;
        private int Width { get; } = 32;
        private int Height { get; } = 32;

        protected override void Draw()
        {
            FillRectangle(X, Y, Width, Height, new Color(0, 255, 0));
        }
    }
}